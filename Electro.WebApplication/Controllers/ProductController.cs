using Electro.Model.Database;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Electro.WebApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataManager _dataManager;

        public ProductController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Route("~/Product/{productId}")]
        public IActionResult Index(Guid productId)
        {
            return View(_dataManager.Products.GetProductById(productId));
        }
    }
}
