using System.Net.Http;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.HttpClients
{
    public class CategoryClient : BaseHttpClient
    {
        public CategoryClient() : base("")
        {

        }

        public async Task<HttpResponseMessage> GetCategories()
        {
            return await _client.GetAsync("catalog/");
        }
    }
}
