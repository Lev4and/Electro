using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class CharacteristicQuantityUnitValue
    {
        public Guid Id { get; set; }

        public Guid CharacteristicQuantityUnitId { get; set; }

        [Required]
        public string Value { get; set; }

        public CharacteristicQuantityUnit CharacteristicQuantityUnit { get; set; }

        public virtual ICollection<ProductCharacteristicQuantityUnitValue> Products { get; set; }
    }
}
