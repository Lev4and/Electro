using Electro.Model.Parsers.DNS.HttpClients;
using System.IO;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.HtmlLoaders
{
    public class CategoryHtmlLoader
    {
        private readonly CategoryClient _client;

        public CategoryHtmlLoader()
        {
            _client = new CategoryClient();
        }

        public async Task<Stream> GetCategories()
        {
            var response = await _client.GetCategories();

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
