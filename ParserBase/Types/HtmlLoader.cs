using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParserBase.Types
{
    class HtmlLoader
    {
        readonly string url;
        readonly HttpClient client;

        public HtmlLoader(IParserSettings parserSettings)
        {
            url = parserSettings.Url;
            client = new HttpClient();
        }

        public async Task<string> GetPageContentById(int id)
        {
            var response = await client.GetAsync(url.Replace("{ID}", id.ToString()));
            var content = string.Empty;

            if(response.Content!=null && response.StatusCode==System.Net.HttpStatusCode.OK)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            return content;
        }
    }
}
