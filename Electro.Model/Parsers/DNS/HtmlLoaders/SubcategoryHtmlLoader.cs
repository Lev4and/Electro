using Electro.Model.Parsers.DNS.HttpClients;
using System.IO;
using System.Threading.Tasks;

namespace Electro.Model.Parsers.DNS.HtmlLoaders
{
    public class SubcategoryHtmlLoader
    {
        private readonly SubcategoryClient _client;

        public SubcategoryHtmlLoader()
        {
            _client = new SubcategoryClient();
        }

        public async Task<Stream> GetSubcategories(string link)
        {
            var response = await _client.GetSubcategories(link);

            if (response != null)
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
