using AngleSharp.Html.Parser;
using System;

namespace ParserBase.Types
{
    public class ParserWorker<T> where T : class
    {
        private IParser<T> parser;
        private IParserSettings parserSettings;
        private HtmlLoader loader;
        private bool isActive;

        public event Action<object, T> OnNewData;
        public event Action<object> OnEndWorking;

        #region Properties

        public IParser<T> Parser { get => parser; set => parser = value; }
        public IParserSettings ParserSettings
        {
            get => parserSettings;
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(parserSettings);
            }
        }
        public bool IsActive => isActive;

        #endregion

        public void Start()
        {
            isActive = true;
            Working();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async void Working()
        {
            for (int i = ParserSettings.StartPoint; i <= ParserSettings.EndPoint; i++)
            {
                if (!IsActive) break;

                var content = await loader.GetPageContentById(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(content);
                var result = parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }

            isActive = false;
            OnEndWorking?.Invoke(this);
        }

        public ParserWorker(IParser<T> parser)
        {
            Parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            ParserSettings = parserSettings;
        }
    }
}
