using Electro.Model.Decompresses.Gzip;
using Electro.Model.Parsers.DNS.HtmlLoaders;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.ParseWorkers
{
    public class CategoryParseWorker<T> : BaseParseWorker<T> where T : class
    {
        private readonly CategoryHtmlLoader _htmlLoader;

        public CategoryParseWorker(IParser<T> parser) : base(parser)
        {
            _htmlLoader = new CategoryHtmlLoader();
        }

        public override async Task<T> DoWork()
        {
            var resultHtml = "";
            var resultSteam = await _htmlLoader.GetCategories();

            using(StreamReader reader = new StreamReader(resultSteam, Encoding.UTF8))
            {
                resultHtml = await reader.ReadToEndAsync();
            }

            var document = await _htmlParser.ParseDocumentAsync(resultHtml, new CancellationTokenSource().Token);
            var result = _parser.Parse(document);

            return result;
        }
    }
}
