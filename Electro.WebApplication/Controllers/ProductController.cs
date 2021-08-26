using Electro.Model.Database;
using Electro.WebApplication.Services.Cookie;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Electro.WebApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly ViewsCookieService _viewsCookieService;

        public ProductController(DataManager dataManager, ViewsCookieService viewsCookieService)
        {
            _dataManager = dataManager;
            _viewsCookieService = viewsCookieService;
        }

        [Route("~/Product/{productId}")]
        public IActionResult Index(Guid productId)
        {
            if (User.Identity.IsAuthenticated)
            {

            }
            else
            {
                _viewsCookieService.AddProductToViews(productId, Request, Response);
            }

            return View(_dataManager.Products.GetProductById(productId));
        }
    }
}
