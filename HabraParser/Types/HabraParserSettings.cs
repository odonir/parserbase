using ParserBase.Types;

namespace HabraParser.Types
{
    class HabraParserSettings : IParserSettings
    {
        public string Url { get; set; } = "https://habr.com/ru/all/page{ID}/";
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }

        public HabraParserSettings(int startPoint, int endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
}
