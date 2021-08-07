using Electro.Model.Parsers.DNS.HtmlLoaders;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.ParseWorkers
{
    public class PaginationParseWorker<T> : BaseParseWorker<T> where T : class
    {
        private readonly PaginationHtmlLoader _htmlLoader;

        public PaginationParseWorker(IParser<T> parser) : base(parser)
        {
            _htmlLoader = new PaginationHtmlLoader();
        }

        public override async Task<T> DoWork(string link)
        {
            var resultHtml = "";
            var resultSteam = await _htmlLoader.GetPagination(link);

            using (StreamReader reader = new StreamReader(resultSteam, Encoding.UTF8))
            {
                resultHtml = await reader.ReadToEndAsync();
            }

            var document = await _htmlParser.ParseDocumentAsync(resultHtml, new CancellationTokenSource().Token);
            var result = _parser.Parse(document);

            return result;
        }
    }
}
