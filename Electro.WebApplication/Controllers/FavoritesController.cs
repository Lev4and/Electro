using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.WebApplication.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly DataManager _dataManager;

        public FavoritesController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Route("~/Favorites")]
        public IActionResult Index()
        {
            var viewModel = new FavoritesViewModel()
            {
                Content = new Dictionary<Product, DateTime>()
                {
                    { _dataManager.Products.GetProductById(Guid.Parse("7b339d95-f6c0-417b-a466-15989c33639f")), DateTime.Now },
                    { _dataManager.Products.GetProductById(Guid.Parse("1a1196e1-cb3d-4ad7-b859-042a935986b9")), DateTime.Now }
                }
            };

            var sortedProducts = viewModel.Content.OrderByDescending(item => item.Value);

            viewModel.Content = sortedProducts
                .ToDictionary<KeyValuePair<Product, DateTime>, Product, DateTime>(pair => pair.Key, pair => pair.Value);

            return View(viewModel);
        }
    }
}
