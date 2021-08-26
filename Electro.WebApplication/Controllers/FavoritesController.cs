using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Electro.WebApplication.Services.Cookie;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.WebApplication.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly FavoritesCookieService _favoritesCookieService;

        public FavoritesController(DataManager dataManager, FavoritesCookieService favoritesCookieService)
        {
            _dataManager = dataManager;
            _favoritesCookieService = favoritesCookieService;
        }

        [Route("~/Favorites")]
        public IActionResult Index()
        {
            var viewModel = new FavoritesViewModel();

            if (User.Identity.IsAuthenticated)
            {

            }
            else
            {
                var favoritesContent = _favoritesCookieService.GetFavoritesContent(Request);
                var favorites = _dataManager.Products.GetProductsByIds(favoritesContent.Keys.ToList())
                    .ToList().ToDictionary(key => key, value => Convert.ToDateTime(value));

                viewModel.Content = favorites.OrderByDescending(keyValuePair =>
                    keyValuePair.Value).ToDictionary<KeyValuePair<Product, DateTime>, Product, DateTime>(pair => pair.Key,
                        pair => pair.Value);
            }

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult Add(Guid productId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return new JsonResult(new { count = 0 });
            }
            else
            {
                return new JsonResult(new { count = _favoritesCookieService.AddProductInFavotites(productId, Request, Response) });
            }
        }
        
        [HttpPost]
        public JsonResult Remove(Guid productId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return new JsonResult(new { count = 0 });
            }
            else
            {
                return new JsonResult(new { count = _favoritesCookieService.RemoveProductFromFavotites(productId, Request, Response) });
            }
        }
    }
}
