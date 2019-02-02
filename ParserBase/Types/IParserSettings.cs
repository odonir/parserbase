using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserBase.Types
{
    public interface IParserSettings
    {
        string Url { get; set; }
        int StartPoint { get; set; }
        int EndPoint { get; set; }
    }
}
