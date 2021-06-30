using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class Characteristic
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<CharacteristicCategory> Categories { get; set; }

        public virtual ICollection<CharacteristicQuantityUnit> QuantityUnits { get; set; }
    }
}
