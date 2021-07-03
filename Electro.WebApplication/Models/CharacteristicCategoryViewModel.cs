using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class CharacteristicCategoryViewModel
    {
        public CharacteristicCategory CharacteristicCategory { get; set; }

        public List<Category> Categories { get; set; }

        public List<SectionCharacteristic> SectionsCharacteristics { get; set; }
    }
}
