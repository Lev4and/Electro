using System.Net.Http;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.HttpClients
{
    public class PaginationClient : BaseHttpClient
    {
        public PaginationClient() : base("")
        {

        }

        public async Task<HttpResponseMessage> GetPagination(string link)
        {
            return await _client.GetAsync(link);
        }
    }
}
