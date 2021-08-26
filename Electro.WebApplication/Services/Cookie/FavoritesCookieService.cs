using Electro.WebApplication.Services.Cookie.JsonConverts;
using Electro.WebApplication.Services.Decoders;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Services.Cookie
{
    public class FavoritesCookieService : ICookieService
    {
        private readonly CookieDecoder _decoder;
        private readonly FavoritesJsonConverter _jsonConverter; 

        public string CookieName { get; }

        public FavoritesCookieService(CookieDecoder decoder, FavoritesJsonConverter jsonConverter)
        {
            _decoder = decoder;
            _jsonConverter = jsonConverter;

            CookieName = "favorites";
        }

        public int GetCountProductsInFavorites(HttpRequest request)
        {
            if(request != null)
            {
                if (request.Cookies.ContainsKey(CookieName))
                {
                    var cookie = _decoder.Decode(request.Cookies[CookieName]);
                    var favorites = _jsonConverter.Deserialize(cookie);

                    return favorites.Count;
                }
            }

            return 0;
        }

        public int AddProductInFavotites(Guid productId, HttpRequest request, HttpResponse response)
        {
            if(request != null && response != null)
            {
                if (request.Cookies.ContainsKey(CookieName))
                {
                    var cookie = _decoder.Decode(request.Cookies["favorites"]);
                    var favorites = _jsonConverter.Deserialize(cookie);

                    if (!favorites.ContainsKey(productId))
                    {
                        favorites.Add(productId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }

                    response.Cookies.Append(CookieName, JsonConvert.SerializeObject(favorites), new CookieOptions 
                    { 
                        Expires = DateTime.Now.AddYears(1) 
                    });

                    return favorites.Keys.Count;
                }
                else
                {
                    var favorites = new Dictionary<Guid, string>()
                    {
                        { productId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
                    };

                    response.Cookies.Append(CookieName, JsonConvert.SerializeObject(favorites), new CookieOptions 
                    { 
                        Expires = DateTime.Now.AddYears(1) 
                    });

                    return favorites.Keys.Count;
                }
            }

            return 0;
        }

        public int RemoveProductFromFavotites(Guid productId, HttpRequest request, HttpResponse response)
        {
            if (request != null && response != null)
            {
                if (request.Cookies.ContainsKey(CookieName))
                {
                    var cookie = _decoder.Decode(request.Cookies[CookieName]);
                    var favorites = _jsonConverter.Deserialize(cookie);

                    if (favorites.ContainsKey(productId))
                    {
                        favorites.Remove(productId);
                    }

                    if (favorites.Count == 0)
                    {
                        response.Cookies.Delete(CookieName);
                    }
                    else
                    {
                        response.Cookies.Append(CookieName, JsonConvert.SerializeObject(favorites), new CookieOptions 
                        { 
                            Expires = DateTime.UtcNow.AddYears(1) 
                        });
                    }

                    return favorites.Keys.Count;
                }
            }

            return 0;
        }

        public Dictionary<Guid, string> GetFavoritesContent(HttpRequest request)
        {
            if (request != null)
            {
                if (request.Cookies.ContainsKey(CookieName))
                {
                    return _jsonConverter.Deserialize(_decoder.Decode(request.Cookies[CookieName]));
                }
            }

            return new Dictionary<Guid, string>();
        }
    }
}
