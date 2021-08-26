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
    public class CompareController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly CompareCookieService _compareCookieService;

        public CompareController(DataManager dataManager, CompareCookieService compareCookieService)
        {
            _dataManager = dataManager;
            _compareCookieService = compareCookieService;
        }

        [Route("~/Compare")]
        public IActionResult Index()
        {
           var viewModel = new CompareViewModel();

            if (User.Identity.IsAuthenticated)
            {

            }
            else
            {
                var compareContent = _compareCookieService.GetCompareContent(Request);

                if(compareContent.Keys.Count == 1)
                {
                    viewModel.Products = _dataManager.Products.GetProductsByIds(compareContent.Values.SelectMany(value => 
                        value).ToList()).ToList();
                    viewModel.CharacteristicCategories = _dataManager.CharacteristicCategories
                        .GetCharacteristicCategoriesByCategoryId(compareContent.ElementAt(0).Key).ToList();
                }
                else
                {
                    viewModel.Products = new List<Product>();
                    viewModel.CharacteristicCategories = new List<CharacteristicCategory>();
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public PartialViewResult DropdownContent()
        {
            var compareContent = _compareCookieService.GetCompareContent(Request);

            return PartialView("_CompareDropdownContentPartial", _dataManager.Products
                .GetProductsByIds(compareContent.Values.SelectMany(value => value).ToList()).ToList());
        }

        [HttpPost]
        public JsonResult Add(Guid categoryId, Guid productId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return new JsonResult(new { count = 0 });
            }
            else
            {
                return new JsonResult(new { count = _compareCookieService.AddProductInCompare(categoryId, productId, Request, Response) });
            }
        }

        [HttpPost]
        public JsonResult Remove(Guid categoryId, Guid productId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return new JsonResult(new { count = 0 });
            }
            else
            {
                return new JsonResult(new { count = _compareCookieService.RemoveProductFromCompare(categoryId, productId, Request, Response) });
            }
        }
    }
}
