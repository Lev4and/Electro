using System;
using System.Net.Http;

namespace Electro.Model.Parsers.DNS
{
    public class BaseHttpClient
    {
        private string _baseUrl;

        private protected HttpClient _client;
        private protected HttpClient _pageClient;
        private protected HttpClient _ajaxClient;
        private protected HttpContent _content;
        private protected HttpClientHandler _handler;

        public BaseHttpClient(string pathAndQueryUrl)
        {
            _baseUrl = $"https://www.dns-shop.ru/{pathAndQueryUrl}";

            _handler = new HttpClientHandler();
            _handler.AllowAutoRedirect = true;
            _handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.Brotli;
            _handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };

            _client = new HttpClient(_handler);
            _pageClient = new HttpClient(_handler);
            _ajaxClient = new HttpClient(_handler);

            _client.BaseAddress = new Uri(_baseUrl);
            _pageClient.BaseAddress = new Uri(_baseUrl);
            _ajaxClient.BaseAddress = new Uri(_baseUrl + "ajax-state/");

            _client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            _client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
            _client.DefaultRequestHeaders.Add("sec-ch-ua", "\" Not; A Brand\";v=\"99\", \"Yandex\";v=\"91\", \"Chromium\";v=\"91\"");
            _client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            _client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            _client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.106 YaBrowser/21.6.0.620 Yowser/2.5 Safari/537.36");
            _client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
            _client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "same-origin");
            _client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            _client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            _client.DefaultRequestHeaders.Add("Accept-Language", "ru,en;q=0.9");

            //_pageClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            //_pageClient.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
            //_pageClient.DefaultRequestHeaders.Add("sec-ch-ua", "\" Not; A Brand\";v=\"99\", \"Yandex\";v=\"91\", \"Chromium\";v=\"91\"");
            //_pageClient.DefaultRequestHeaders.Add("X-CSRF-Token", "");
            //_pageClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            //_pageClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.106 YaBrowser/21.6.0.620 Yowser/2.5 Safari/537.36");
            //_pageClient.DefaultRequestHeaders.Add("Accept", "*/*");
            //_pageClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
            //_pageClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            //_pageClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            //_pageClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            //_pageClient.DefaultRequestHeaders.Add("Accept-Language", "ru,en;q=0.9");

            _ajaxClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            _ajaxClient.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
            _ajaxClient.DefaultRequestHeaders.Add("sec-ch-ua", "\" Not; A Brand\";v=\"99\", \"Yandex\";v=\"91\", \"Chromium\";v=\"91\"");
            _ajaxClient.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
            _ajaxClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            _ajaxClient.DefaultRequestHeaders.Add("Accept", "*/*");
            _ajaxClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.106 YaBrowser/21.6.0.620 Yowser/2.5 Safari/537.36");
            _ajaxClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
            _ajaxClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            _ajaxClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            _ajaxClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            _ajaxClient.DefaultRequestHeaders.Add("Accept-Language", "ru,en;q=0.9");
        }
    }
}
