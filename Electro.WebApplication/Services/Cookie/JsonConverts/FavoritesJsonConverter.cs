using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Services.Cookie.JsonConverts
{
    public class FavoritesJsonConverter
    {
        public Dictionary<Guid, string> Deserialize(string cookie)
        {
            return JsonConvert.DeserializeObject<Dictionary<Guid, string>>(cookie);
        }
    }
}
