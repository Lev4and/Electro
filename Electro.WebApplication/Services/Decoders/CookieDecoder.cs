using System.Web;

namespace Electro.WebApplication.Services.Decoders
{
    public class CookieDecoder
    {
        public string Decode(string cookieContent)
        {
            return HttpUtility.UrlDecode(cookieContent);
        }
    }
}
