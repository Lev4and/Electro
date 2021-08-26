using Electro.WebApplication.Services.Cookie.JsonConverts;
using Electro.WebApplication.Services.Decoders;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Services.Cookie
{
    public class ViewsCookieService : ICookieService
    {
        private readonly CookieDecoder _decoder;
        private readonly ViewsJsonConverter _jsonConverter;

        public string CookieName { get; }

        public ViewsCookieService(CookieDecoder decoder, ViewsJsonConverter jsonConverter)
        {
            _decoder = decoder;
            _jsonConverter = jsonConverter;

            CookieName = "views";
        }

        public void AddProductToViews(Guid productId, HttpRequest request, HttpResponse response)
        {
            if(request != null && response != null)
            {
                if (request.Cookies.ContainsKey(CookieName))
                {
                    var cookie = _decoder.Decode(request.Cookies[CookieName]);
                    var views = _jsonConverter.Deserialize(cookie);

                    if (views.ContainsKey(DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        views[DateTime.Now.ToString("yyyy-MM-dd")].Add(productId);
                    }
                    else
                    {
                        views.Add(DateTime.Now.ToString("yyyy-MM-dd"), new List<Guid>() { productId });
                    }

                    response.Cookies.Append(CookieName, JsonConvert.SerializeObject(views), new CookieOptions() 
                    { 
                        Expires = DateTime.Now.AddYears(1) 
                    });
                }
                else
                {
                    var views = new Dictionary<string, List<Guid>>()
                    {
                        { DateTime.Now.ToString("yyyy-MM-dd"), new List<Guid>(){ productId } }
                    };

                    response.Cookies.Append(CookieName, JsonConvert.SerializeObject(views), new CookieOptions() 
                    { 
                        Expires = DateTime.Now.AddYears(1) 
                    });
                }
            }
        }
    }
}
