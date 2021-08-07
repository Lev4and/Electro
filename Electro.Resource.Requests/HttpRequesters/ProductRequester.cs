using Electro.Model.Database.Entities;
using Electro.Resource.Requests.HttpResponseLoaders;
using System.IO;
using System.Threading.Tasks;

namespace Electro.Resource.Requests.HttpRequesters
{
    public class ProductRequester
    {
        private readonly ProductResponseLoader _responseLoader;

        public ProductRequester()
        {
            _responseLoader = new ProductResponseLoader();
        }

        public async Task<bool> SaveProductAsync(Product product)
        {
            var resultStream = await _responseLoader.GetStreamAsync(product);

            if(resultStream != null)
            {
                using(var stream = new StreamReader(resultStream))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
