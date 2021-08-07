using Electro.Model.Database.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Electro.Resource.Requests.HttpClients
{
    public class ProductClient : BaseHttpClient
    {
        public ProductClient() : base("product/")
        {

        }

        public async Task<HttpResponseMessage> SaveProductAsync(Product product)
        {
            return await _client.PostAsync("save", new StringContent(JsonConvert.SerializeObject(product), 
                Encoding.UTF8, "application/json"));
        }
    }
}
