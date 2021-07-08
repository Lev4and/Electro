using System;

namespace Electro.Model.Database.Entities
{
    public class ProductCharacteristicCategoryValue
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid CharacteristicCategoryValueId { get; set; }

        public virtual Product Product { get; set; }

        public virtual CharacteristicCategoryValue CharacteristicCategoryValue { get; set; }
    }
}
