using Electro.Model.Database;
using Electro.WebApplication.Models;
using Electro.WebApplication.Services.Cookie;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Electro.WebApplication.Components
{
    public class HeaderIcons : ViewComponent
    {
        private readonly DataManager _dataManager;
        private readonly CartCookieService _cartCookieService;
        private readonly CompareCookieService _compareCookieService;
        private readonly FavoritesCookieService _favoritesCookieService;

        public HeaderIcons(DataManager dataManager, CartCookieService cartCookieService, CompareCookieService compareCookieService, FavoritesCookieService favoritesCookieService)
        {
            _dataManager = dataManager;
            _cartCookieService = cartCookieService;
            _compareCookieService = compareCookieService;
            _favoritesCookieService = favoritesCookieService;
        }

        public IViewComponentResult Invoke(HttpRequest request)
        {
            var cartContent = _cartCookieService.GetCartContent(request);
            var compareContent = _compareCookieService.GetCompareContent(request);

            var countProductsInCart = _cartCookieService.GetCountProductsInCart(request);
            var countProductsInCompare = _compareCookieService.GetCountProductsInCompare(request);
            var countProductsInFavorites = _favoritesCookieService.GetCountProductsInFavorites(request);

            var viewModel = new HeaderIconsViewModel()
            {
                CountProductsInCart = countProductsInCart,
                CountProductsInCompare = countProductsInCompare,
                CountProductsInFavorites = countProductsInFavorites,
                CartContent = _dataManager.Products.GetProductsByIds(cartContent.Keys.ToList())
                    .ToDictionary(key => key, value => cartContent[value.Id]),
                CompareContent = _dataManager.Products.GetProductsByIds(compareContent.Values.SelectMany(value => 
                    value).ToList()).ToList()
            };

            return View("Default", viewModel);
        }
    }
}
