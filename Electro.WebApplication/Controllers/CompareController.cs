using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.WebApplication.Controllers
{
    public class CompareController : Controller
    {
        private readonly DataManager _dataManager;

        public CompareController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Route("~/Compare")]
        public IActionResult Index()
        {
            var viewModel = new CompareViewModel()
            {
                Products = _dataManager.Products.GetLatestProductsByCategoryId(Guid.Parse("a232782b-431a-43e9-038d-08d94b6c4391"), 
                    4).ToList(),
                CharacteristicCategories = _dataManager.CharacteristicCategories
                    .GetCharacteristicCategoriesByCategoryId(Guid.Parse("a232782b-431a-43e9-038d-08d94b6c4391")).ToList()
            };

            return View(viewModel);
        }
    }
}
