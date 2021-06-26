using System;

namespace Electro.Model.Database.Entities
{
    public class ProductCharacteristicQuantityUnitValue
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid CharacteristicQuantityUnitValueId { get; set; }

        public virtual Product Product { get; set; }

        public virtual CharacteristicQuantityUnitValue CharacteristicQuantityUnitValue { get; set; }
    }
}
