using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Services.Cookie.JsonConverts
{
    public class ViewsJsonConverter
    {
        public Dictionary<string, List<Guid>> Deserialize(string cookie)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, List<Guid>>>(cookie);
        }
    }
}
