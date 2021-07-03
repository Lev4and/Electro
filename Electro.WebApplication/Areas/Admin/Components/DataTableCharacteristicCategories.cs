using Electro.Model.Database;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Electro.WebApplication.Areas.Admin.Components
{
    public class DataTableCharacteristicCategories : ViewComponent
    {
        private readonly DataManager _dataManager;

        public DataTableCharacteristicCategories(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IViewComponentResult Invoke(Guid characteristicId)
        {
            var viewModel = new CharacteristicCategoriesViewModel()
            {
                CharacteristicCategories = _dataManager.CharacteristicCategories
                    .GetCharacteristicCategoriesByCharacteristicId(characteristicId).ToList()
            };

            return View("Default", viewModel);
        }
    }
}
