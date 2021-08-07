using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly DataManager _dataManager;

        public CartController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Route("~/Cart")]
        public IActionResult Index()
        {
            var viewModel = new CartViewModel()
            {
                Content = new Dictionary<Product, int>()
                {
                    { _dataManager.Products.GetProductById(Guid.Parse("1a1196e1-cb3d-4ad7-b859-042a935986b9")), 1 },
                    { _dataManager.Products.GetProductById(Guid.Parse("7b339d95-f6c0-417b-a466-15989c33639f")), 2 }
                }
            };

            return View(viewModel);
        }
    }
}
