using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class SectionCharacteristicViewModel
    {
        public SectionCharacteristic SectionCharacteristic { get; set; }

        public List<Guid> SelectedCategories { get; set; }

        public List<Category> UsedCategories { get; set; }

        public List<Category> NotUsedCategoriesForSectionCharacteristic { get; set; }


    }
}
