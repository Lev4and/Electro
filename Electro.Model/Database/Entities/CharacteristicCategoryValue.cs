using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class CharacteristicCategoryValue
    {
        public Guid Id { get; set; }

        public Guid CharacteristicCategoryId { get; set; }

        [Required]
        public string Value { get; set; }

        public virtual CharacteristicCategory CharacteristicCategory { get; set; }

        public virtual ICollection<ProductCharacteristicCategoryValue> Products { get; set; }
    }
}
