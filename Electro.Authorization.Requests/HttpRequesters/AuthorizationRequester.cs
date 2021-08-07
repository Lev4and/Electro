using Electro.Authorization.Models;
using Electro.Authorization.Requests.HttpResponseLoaders;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Electro.Authorization.Requests.HttpRequesters
{
    public class AuthorizationRequester
    {
        private readonly AuthorizationResponseLoader _responseLoader;

        public AuthorizationRequester()
        {
            _responseLoader = new AuthorizationResponseLoader();
        }

        public async Task<JWTViewModel> GetAccessTokenAsync(LoginViewModel viewModel)
        {
            var jWT = new JWTViewModel();

            var resultJson = "";
            var resultStream = await _responseLoader.GetStreamAsync(viewModel);

            if(resultStream != null)
            {
                using(var stream = new StreamReader(resultStream))
                {
                    resultJson = await stream.ReadToEndAsync();
                }

                jWT = JsonConvert.DeserializeObject<JWTViewModel>(resultJson);
            }

            return jWT;
        }
    }
}
