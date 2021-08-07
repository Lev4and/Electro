using Electro.Authorization.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Electro.Authorization.Requests.HttpClients
{
    public class AuthorizationClient : BaseHttpClient
    {
        public AuthorizationClient() : base("Authorization/")
        {

        }

        public async Task<HttpResponseMessage> LoginAsync(LoginViewModel viewModel)
        {
            return await _client.PostAsync("Login", new StringContent(JsonConvert.SerializeObject(viewModel), 
                Encoding.UTF8, "application/json"));
        }
    }
}
