using Electro.Model.Parsers.DNS.HtmlLoaders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.ParseWorkers
{
    public class ProductsParseWorker<T> : BaseParseWorker<T> where T : class
    {
        private readonly ProductsHtmlLoader _htmlLoader;

        public ProductsParseWorker(IParser<T> parser) : base(parser)
        {
            _htmlLoader = new ProductsHtmlLoader();
        }

        public override async Task<T> DoWork(string link, int numberPage)
        {
            var resultHtml = "";
            var resultSteam = await _htmlLoader.GetProducts(link, numberPage);

            using (StreamReader reader = new StreamReader(resultSteam, Encoding.UTF8))
            {
                resultHtml = await reader.ReadToEndAsync();
            }

            var document = await _htmlParser.ParseDocumentAsync(resultHtml, new CancellationTokenSource().Token);
            var result = _parser.Parse(document);

            return result;
        }

        public override async Task<T> DoWork(string link, int numberPage, string token)
        {
            var resultHtml = "";
            var resultJson = "";
            var resultSteam = await _htmlLoader.GetProducts(link, numberPage, token);

            using (StreamReader reader = new StreamReader(resultSteam, Encoding.UTF8))
            {
                resultJson = await reader.ReadToEndAsync();
            }

            resultHtml = JsonConvert.DeserializeObject<dynamic>(resultJson).html;

            var document = await _htmlParser.ParseDocumentAsync(resultHtml, new CancellationTokenSource().Token);
            var result = _parser.Parse(document);

            return result;
        }
    }
}
