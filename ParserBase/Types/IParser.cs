using AngleSharp.Html.Dom;

namespace ParserBase.Types
{
    public interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
