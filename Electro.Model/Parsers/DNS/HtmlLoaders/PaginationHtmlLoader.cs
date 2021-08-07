using Electro.Model.Parsers.DNS.HttpClients;
using System.IO;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.HtmlLoaders
{
    public class PaginationHtmlLoader
    {
        private readonly PaginationClient _client;

        public PaginationHtmlLoader()
        {
            _client = new PaginationClient();
        }

        public async Task<Stream> GetPagination(string link)
        {
            var response = await _client.GetPagination(link);

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
