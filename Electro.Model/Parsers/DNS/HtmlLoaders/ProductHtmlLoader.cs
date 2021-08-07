using Electro.Model.Parsers.DNS.HttpClients;
using System.IO;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.HtmlLoaders
{
    public class ProductHtmlLoader
    {
        private readonly ProductClient _client;

        public ProductHtmlLoader()
        {
            _client = new ProductClient();
        }

        public async Task<Stream> GetProduct(string link)
        {
            var response = await _client.GetProduct(link);

            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStreamAsync();
                }
            }

            return null;
        }

        public async Task<Stream> GetProductImagesSlider(string containerId, string productId)
        {
            var response = await _client.GetProductImagesSlider(containerId, productId);

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
