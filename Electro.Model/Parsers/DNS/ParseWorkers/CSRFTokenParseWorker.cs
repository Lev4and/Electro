using Electro.Model.Parsers.DNS.HtmlLoaders;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.ParseWorkers
{
    public class CSRFTokenParseWorker<T> : BaseParseWorker<T> where T : class
    {
        private readonly CSRFTokenHtmlLoader _htmlLoader;

        public CSRFTokenParseWorker(IParser<T> parser) : base(parser)
        {
            _htmlLoader = new CSRFTokenHtmlLoader();
        }

        public override async Task<T> DoWork(string link, int numberPage = 1)
        {
            var resultHtml = "";
            var resultSteam = await _htmlLoader.GetToken(link, numberPage);

            if (resultSteam != null)
            {
                using (StreamReader reader = new StreamReader(resultSteam, Encoding.UTF8))
                {
                    resultHtml = await reader.ReadToEndAsync();
                }

                var document = await _htmlParser.ParseDocumentAsync(resultHtml, new CancellationTokenSource().Token);
                var result = _parser.Parse(document);

                return result;
            }

            return null;
        }
    }
}
