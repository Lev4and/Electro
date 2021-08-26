using Electro.Model.Database;
using Electro.WebApplication.Models;
using Electro.WebApplication.Services.Cookie;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Electro.WebApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly CartCookieService _cartCookieService;

        public CartController(DataManager dataManager, CartCookieService cartCookieService)
        {
            _dataManager = dataManager;
            _cartCookieService = cartCookieService;
        }

        [Route("~/Cart")]
        public IActionResult Index()
        {
            var viewModel = new CartViewModel();

            if (User.Identity.IsAuthenticated)
            {

            }
            else
            {
                var cartContent = _cartCookieService.GetCartContent(Request);

                viewModel.Content = _dataManager.Products.GetProductsByIds(cartContent.Keys.ToList())
                    .ToDictionary(key => key, value => cartContent[value.Id]);
            }

            return View(viewModel);
        }

        [HttpPost]
        public PartialViewResult DropdownContent()
        {
            var cartContent = _cartCookieService.GetCartContent(Request);

            return PartialView("_CartDropdownContentPartial", _dataManager.Products.GetProductsByIds(cartContent.Keys.ToList())
                    .ToDictionary(key => key, value => cartContent[value.Id]));
        }

        [HttpPost]
        public JsonResult Add(Guid productId, int quantity = 1)
        {
            return new JsonResult(new { count = _cartCookieService.AddProductInCart(productId, quantity, Request, Response) });
        }

        [HttpPost]
        public JsonResult Remove(Guid productId)
        {
            return new JsonResult(new { count = _cartCookieService.RemoveProductFromCart(productId, Request, Response) });
        }
    }
}
