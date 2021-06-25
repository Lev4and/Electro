using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class Manufacturer
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ManufacturerLogo Logo { get; set; }

        public ManufacturerInformation Information { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
