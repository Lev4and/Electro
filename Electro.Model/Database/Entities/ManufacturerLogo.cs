using System;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class ManufacturerLogo
    {
        public Guid Id { get; set; }

        public Guid ManufacturerId { get; set; }

        [Required]
        public string Url { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
