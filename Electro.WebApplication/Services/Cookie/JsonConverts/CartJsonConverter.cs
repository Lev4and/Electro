using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Services.Cookie.JsonConverts
{
    public class CartJsonConverter
    {
        public Dictionary<Guid, int> Deserialize(string cookie)
        {
            return JsonConvert.DeserializeObject<Dictionary<Guid, int>>(cookie);
        }
    }
}
