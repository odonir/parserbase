using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using ParserBase.Types;

namespace HabraParser.Types
{
    class HabraParser : IParser<Tuple<string, string>[]>
    {
        public Tuple<string, string>[] Parse(IHtmlDocument document)
        {
            var list = new List<Tuple<string, string>>();

            var items = 
                document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("post__title_link"));

            foreach(var item in items)
            {
                list.Add(new Tuple<string, string>(item.TextContent, item.GetAttribute("href")));
            }

            return list.ToArray();
        }
    }
}
