using Electro.Model.Parsers.DNS.HttpClients;
using System.IO;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.HtmlLoaders
{
    public class ProductsHtmlLoader
    {
        private readonly ProductsClient _client;

        public ProductsHtmlLoader()
        {
            _client = new ProductsClient();
        }

        public async Task<Stream> GetProducts(string link, int numberPage)
        {
            var response = await _client.GetProducts(link, numberPage);

            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStreamAsync();
                }
            }

            return null;
        }

        public async Task<Stream> GetProducts(string link, int numberPage, string token)
        {
            var response = await _client.GetProducts(link, numberPage, token);

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
