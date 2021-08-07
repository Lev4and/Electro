using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.HttpClients
{
    public class ProductsClient : BaseHttpClient
    {
        public ProductsClient() : base("")
        {

        }

        public async Task<HttpResponseMessage> GetProducts(string link, int numberPage)
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

        public async Task<HttpResponseMessage> GetProducts(string link, int numberPage, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _pageClient.DefaultRequestHeaders.Clear();

                _pageClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                _pageClient.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
                _pageClient.DefaultRequestHeaders.Add("sec-ch-ua", "\" Not; A Brand\";v=\"99\", \"Yandex\";v=\"91\", \"Chromium\";v=\"91\"");
                _pageClient.DefaultRequestHeaders.Add("X-CSRF-Token", token);
                _pageClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                _pageClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.106 YaBrowser/21.6.0.620 Yowser/2.5 Safari/537.36");
                _pageClient.DefaultRequestHeaders.Add("Accept", "*/*");
                _pageClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                _pageClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
                _pageClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
                _pageClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                _pageClient.DefaultRequestHeaders.Add("Accept-Language", "ru,en;q=0.9");
            }

            if (!link.Contains("?"))
            {
                return await _pageClient.GetAsync($"{link}?p={numberPage}");
            }
            else
            {
                var paramsRequest = link.Substring(link.IndexOf("?") + 1).Split('&').ToList();

                if (paramsRequest.Count == 1)
                {
                    return await _pageClient.GetAsync($"{link}&p={numberPage}");
                }
                else
                {
                    paramsRequest.Remove(paramsRequest.FirstOrDefault(param => param.Contains("virtual_category_uid")));

                    return await _pageClient.GetAsync($"{link}&p={numberPage}");
                }
            }
        }
    }
}
