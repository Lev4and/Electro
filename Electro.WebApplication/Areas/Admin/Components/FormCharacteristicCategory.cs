using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.WebApplication.Areas.Admin.Components
{
    public class FormCharacteristicCategory : ViewComponent
    {
        private readonly DataManager _dataManager;

        public FormCharacteristicCategory(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IViewComponentResult Invoke(Guid characteristicId)
        {
            var viewModel = new CharacteristicCategoryViewModel()
            {
                CharacteristicCategory = new CharacteristicCategory()
                {
                    CharacteristicId = characteristicId
                },
                SectionsCharacteristics = new List<SectionCharacteristic>(),
                Categories = _dataManager.Categories.GetCategories().ToList()
            };

            return View("Default", viewModel);
        }
    }
}
