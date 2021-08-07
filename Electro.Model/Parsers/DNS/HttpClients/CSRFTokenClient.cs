using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.HttpClients
{
    public class CSRFTokenClient : BaseHttpClient
    {
        public CSRFTokenClient() : base("")
        {

        }

        public async Task<HttpResponseMessage> GetToken(string link, int numberPage)
        {
            if (!link.Contains("?"))
            {
                return await _client.GetAsync($"{link}?p={numberPage}");
            }
            else
            {
                var paramsRequest = link.Substring(link.IndexOf("?") + 1).Split('&').ToList();

                if (paramsRequest.Count == 1)
                {
                    return await _client.GetAsync($"{link}&p={numberPage}");
                }
                else
                {
                    paramsRequest.Remove(paramsRequest.FirstOrDefault(param => param.Contains("virtual_category_uid")));

                    return await _client.GetAsync($"{link}&p={numberPage}");
                }
            }
        }
    }
}
