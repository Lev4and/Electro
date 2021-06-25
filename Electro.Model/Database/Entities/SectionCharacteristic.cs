using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class SectionCharacteristic
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<CharacteristicCategory> Characteristics { get; set; }

        public virtual ICollection<SectionCharacteristicCategory> Categories { get; set; }
    }
}
