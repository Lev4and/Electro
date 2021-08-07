using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.HttpClients
{
    public class ProductClient : BaseHttpClient
    {
        public ProductClient() : base("")
        {

        }

        public async Task<HttpResponseMessage> GetProduct(string link)
        {
            return await _client.GetAsync(link + "characteristics/");
        }

        public async Task<HttpResponseMessage> GetProductImagesSlider(string containerId, string productId)
        {
            return await _ajaxClient.GetAsync("c-product-images-slider/?data={\"type\":\"product-images-slider\",\"containers\":[{\"id\":\"" + containerId + "\",\"data\":{\"id\":\"" + productId + "\"}}]}");
        }
    }
}
