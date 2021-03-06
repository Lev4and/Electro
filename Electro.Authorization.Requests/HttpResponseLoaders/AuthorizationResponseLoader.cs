using Electro.Authorization.Models;
using Electro.Authorization.Requests.HttpClients;
using System.IO;
using System.Threading.Tasks;

namespace Electro.Authorization.Requests.HttpResponseLoaders
{
    public class AuthorizationResponseLoader
    {
        private readonly AuthorizationClient _client;

        public AuthorizationResponseLoader()
        {
            _client = new AuthorizationClient();
        }

        public async Task<Stream> GetStreamAsync(LoginViewModel viewModel)
        {
            var response = await _client.LoginAsync(viewModel);

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
