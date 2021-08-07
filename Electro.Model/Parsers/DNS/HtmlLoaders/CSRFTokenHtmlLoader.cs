using Electro.Model.Parsers.DNS.HttpClients;
using System.IO;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.HtmlLoaders
{
    public class CSRFTokenHtmlLoader
    {
        private readonly CSRFTokenClient _client;

        public CSRFTokenHtmlLoader()
        {
            _client = new CSRFTokenClient();
        }

        public async Task<Stream> GetToken(string link, int numberPage)
        {
            var response = await _client.GetToken(link, numberPage);

            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStreamAsync();
                }
            }

            return null;
        }
    }
}
