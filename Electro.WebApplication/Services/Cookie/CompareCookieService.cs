using Electro.WebApplication.Services.Cookie.JsonConverts;
using Electro.WebApplication.Services.Decoders;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.WebApplication.Services.Cookie
{
    public class CompareCookieService : ICookieService
    {
        private readonly CookieDecoder _decoder;
        private readonly CompareJsonConverter _jsonConverter;

        public string CookieName { get; }

        public CompareCookieService(CookieDecoder decoder, CompareJsonConverter jsonConverter)
        {
            _decoder = decoder;
            _jsonConverter = jsonConverter;

            CookieName = "compare";
        }

        public int GetCountProductsInCompare(HttpRequest request)
        {
            if (request != null)
            {
                if (request.Cookies.ContainsKey(CookieName))
                {
                    var cookie = _decoder.Decode(request.Cookies[CookieName]);
                    var compare = _jsonConverter.Deserialize(cookie);

                    return compare.Values.Sum(value => value.Count);
                }
            }

            return 0;
        }

        public int AddProductInCompare(Guid categoryId, Guid productId, HttpRequest request, HttpResponse response)
        {
            if (request != null && response != null)
            {
                if (request.Cookies.ContainsKey(CookieName))
                {
                    var cookie = _decoder.Decode(request.Cookies[CookieName]);
                    var compare = _jsonConverter.Deserialize(cookie);

                    if (compare.ContainsKey(categoryId) ? !compare[categoryId].Contains(productId) : false)
                    {
                        compare[categoryId].Add(productId);
                    }
                    else
                    {
                        compare = new Dictionary<Guid, List<Guid>>() { { categoryId, new List<Guid>() { productId } } };
                    }

                    response.Cookies.Append(CookieName, JsonConvert.SerializeObject(compare), new CookieOptions()
                    {
                        Expires = DateTime.Now.AddYears(1)
                    });

                    return compare[categoryId].Count;
                }
                else
                {
                    var compare = new Dictionary<Guid, List<Guid>>() { { categoryId, new List<Guid>() { productId } } };

                    response.Cookies.Append(CookieName, JsonConvert.SerializeObject(compare), new CookieOptions()
                    {
                        Expires = DateTime.Now.AddYears(1)
                    });

                    return compare[categoryId].Count;
                }
            }

            return 0;
        }

        public int RemoveProductFromCompare(Guid categoryId, Guid productId, HttpRequest request, HttpResponse response)
        {
            if (request != null && response != null)
            {
                if (request.Cookies.ContainsKey(CookieName))
                {
                    var cookie = _decoder.Decode(request.Cookies[CookieName]);
                    var compare = _jsonConverter.Deserialize(cookie);

                    if (compare.ContainsKey(categoryId) ? compare[categoryId].Contains(productId) : false)
                    {
                        compare[categoryId].Remove(productId);

                        if (compare[categoryId].Count == 0)
                        {
                            response.Cookies.Delete("compare");

                            return 0;
                        }
                        else
                        {
                            response.Cookies.Append("compare", JsonConvert.SerializeObject(compare), new CookieOptions()
                            {
                                Expires = DateTime.Now.AddYears(1)
                            });
                        }

                        return compare[categoryId].Count;
                    }
                }
            }

            return 0;
        }

        public Dictionary<Guid, List<Guid>> GetCompareContent(HttpRequest request)
        {
            if (request != null)
            {
                if (request.Cookies.ContainsKey(CookieName))
                {
                    return _jsonConverter.Deserialize(_decoder.Decode(request.Cookies[CookieName]));
                }
            }

            return new Dictionary<Guid, List<Guid>>();
        }
    }
}
