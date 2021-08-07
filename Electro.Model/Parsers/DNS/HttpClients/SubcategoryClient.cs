using System.Net.Http;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.HttpClients
{
    public class SubcategoryClient : BaseHttpClient
    {
        public SubcategoryClient() : base("")
        {

        }

        public async Task<HttpResponseMessage> GetSubcategories(string link)
        {
            return await _client.GetAsync(link);
        }
    }
}
