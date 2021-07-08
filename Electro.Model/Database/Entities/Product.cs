using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public Guid ManufacturerId { get; set; }

        [Required]
        public string Model { get; set; }

        public double Price { get; set; }

        public virtual Category Category { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ProductMainPhoto MainPhoto { get; set; }

        public virtual ProductInformation Information { get; set; }

        public virtual ICollection<ProductPhoto> Photos { get; set; }

        public virtual ICollection<ProductCharacteristicCategoryValue> CharacteristicsValues { get; set; }
    }
}
