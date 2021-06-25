using System;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class ManufacturerInformation
    {
        public Guid Id { get; set; }

        public Guid ManufacturerId { get; set; }

        [Required]
        public string Description { get; set; }

        public Manufacturer Manufacturer { get; set; }
    }
}
