using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class ProductCharacteristicValueViewModel
    {
        public bool UseNewValue { get; set; }

        public Guid ProductId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid CharacteristicId { get; set; }

        public CharacteristicCategoryValue NewCharacteristicValue { get; set; }

        public ProductCharacteristicCategoryValue ProductCharacteristicValue { get; set; }

        public List<Characteristic> Characteristics { get; set; }

        public List<CharacteristicCategoryValue> CharacteristicValues { get; set; }
    }
}
