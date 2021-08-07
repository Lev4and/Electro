using Electro.Model.Database.Entities;
using Electro.Resource.Requests.HttpClients;
using System.IO;
using System.Threading.Tasks;

namespace Electro.Resource.Requests.HttpResponseLoaders
{
    public class ProductResponseLoader
    {
        private readonly ProductClient _client;

        public ProductResponseLoader()
        {
            _client = new ProductClient();
        }

        public async Task<Stream> GetStreamAsync(Product product)
        {
            var response = await _client.SaveProductAsync(product);

            if(response != null)
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
