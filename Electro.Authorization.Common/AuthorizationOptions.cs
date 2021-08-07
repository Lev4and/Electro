using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Electro.Authorization.Common
{
    public class AuthorizationOptions
    {
        public static int TokenLifetime { get; set; }

        public static string Secret { get; set; }

        public static string Issuer { get; set; }

        public static string Audience { get; set; }

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
