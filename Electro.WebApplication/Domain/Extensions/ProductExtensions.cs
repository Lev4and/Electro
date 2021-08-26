using Electro.Model.Database.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;

namespace Electro.WebApplication.Domain.Extensions
{
    public static class ProductExtensions
    {
        public static string ContainsInCart(this Product product, HttpRequest request)
        {
            if(request != null)
            {
                if (request.Cookies.ContainsKey("cart"))
                {
                    var cookie = HttpUtility.UrlDecode(request.Cookies["cart"]);
                    var cart = JsonConvert.DeserializeObject<Dictionary<Guid, int>>(cookie);

                    return cart.ContainsKey(product.Id) ? "true" : "false";
                }
            }

            return "false";
        }

        public static string ContainsInCompare(this Product product, HttpRequest request)
        {
            if (request != null)
            {
                if (request.Cookies.ContainsKey("compare"))
                {
                    var cookie = HttpUtility.UrlDecode(request.Cookies["compare"]);
                    var compare = JsonConvert.DeserializeObject<Dictionary<Guid, List<Guid>>>(cookie);

                    if (compare.ContainsKey(product.CategoryId))
                    {
                        return compare[product.CategoryId].Contains(product.Id) ? "true" : "false";
                    }
                }
            }

            return "false";
        }

        public static string ContainsInFavorites(this Product product, HttpRequest request)
        {
            if (request != null)
            {
                if (request.Cookies.ContainsKey("favorites"))
                {
                    var cookie = HttpUtility.UrlDecode(request.Cookies["favorites"]);
                    var favorites = JsonConvert.DeserializeObject<Dictionary<Guid, string>>(cookie);

                    return favorites.ContainsKey(product.Id) ? "true" : "false";
                }
            }

            return "false";
        }
    }
}
