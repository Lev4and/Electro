using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Services.Cookie.JsonConverts
{
    public class CompareJsonConverter
    {
        public Dictionary<Guid, List<Guid>> Deserialize(string cookie)
        {
            return JsonConvert.DeserializeObject<Dictionary<Guid, List<Guid>>>(cookie);
        }
    }
}
