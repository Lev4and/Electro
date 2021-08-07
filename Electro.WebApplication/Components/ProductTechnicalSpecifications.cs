using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.WebApplication.Components
{
    public class ProductTechnicalSpecifications : ViewComponent
    {
        public IViewComponentResult Invoke(Product product)
        {
            var dictionary = new Dictionary<string, List<CharacteristicCategoryValue>>();

            foreach(var productCharacteristicCategoryValue in product.CharacteristicsValues)
            {
                var characteristicCategoryValue = productCharacteristicCategoryValue.CharacteristicCategoryValue;
                var characteristicCategory = characteristicCategoryValue.CharacteristicCategory;
                var section = characteristicCategory.Section;

                if (dictionary.ContainsKey(section.Name))
                {
                    dictionary.FirstOrDefault(item => item.Key == section.Name).Value
                        .Add(characteristicCategoryValue);
                }
                else
                {
                    dictionary.Add(section.Name, new List<CharacteristicCategoryValue>() { characteristicCategoryValue });
                }
            }

            return View("Default", dictionary);
        }
    }
}
