using Electro.WebApplication.Services.Cookie.JsonConverts;
using Electro.WebApplication.Services.Decoders;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Services.Cookie
{
    public class CartCookieService : ICookieService
    {
        private readonly CookieDecoder _decoder;
        private readonly CartJsonConverter _jsonConverter;

        public string CookieName { get; }

        public CartCookieService(CookieDecoder decoder, CartJsonConverter jsonConverter)
        {
            _decoder = decoder;
            _jsonConverter = jsonConverter;

            CookieName = "cart";
        }

        public int GetCountProductsInCart(HttpRequest request)
        {
            if (request != null)
            {
                if (request.Cookies.ContainsKey(CookieName))
                {
                    var cookie = _decoder.Decode(request.Cookies[CookieName]);
                    var cart = _jsonConverter.Deserialize(cookie);

                    return cart.Count;
                }
            }

            return 0;
        }

        public int AddProductInCart(Guid productId, int quantity, HttpRequest request, HttpResponse response)
        {
            if(request != null && response != null)
            {
                if (request.Cookies.ContainsKey(CookieName))
                {
                    var cookie = _decoder.Decode(request.Cookies[CookieName]);
                    var cart = _jsonConverter.Deserialize(cookie);

                    if (cart.ContainsKey(productId))
                    {
                        cart[productId] += quantity;
                    }
                    else
                    {
                        cart.Add(productId, quantity);
                    }

                    response.Cookies.Append(CookieName, JsonConvert.SerializeObject(cart), new CookieOptions() 
                    { 
                        Expires = DateTime.Now.AddYears(1) 
                    });

                    return cart.Count;
                }
                else
                {
                    var cart = new Dictionary<Guid, int>() { { productId, quantity } };

                    response.Cookies.Append(CookieName, JsonConvert.SerializeObject(cart), new CookieOptions() 
                    { 
                        Expires = DateTime.Now.AddYears(1) 
                    });

                    return cart.Count;
                }
            }

            return 0;
        }

        public int RemoveProductFromCart(Guid productId, HttpRequest request, HttpResponse response)
        {
            if(request != null && response != null)
            {
                if (request.Cookies.ContainsKey(CookieName))
                {
                    var cookie = _decoder.Decode(request.Cookies[CookieName]);
                    var cart = _jsonConverter.Deserialize(cookie);

                    if (cart.ContainsKey(productId))
                    {
                        cart.Remove(productId);
                    }

                    response.Cookies.Append(CookieName, JsonConvert.SerializeObject(cart), new CookieOptions()
                    {
                        Expires = DateTime.Now.AddYears(1)
                    });

                    return cart.Count;
                }
            }

            return 0;
        }

        public Dictionary<Guid, int> GetCartContent(HttpRequest request)
        {
            if(request != null)
            {
                if (request.Cookies.ContainsKey(CookieName))
                {
                    return _jsonConverter.Deserialize(_decoder.Decode(request.Cookies[CookieName]));
                }
            }

            return new Dictionary<Guid, int>();
        }
    }
}
