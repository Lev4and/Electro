using System;
using System.Collections.Generic;

namespace Electro.Model.Database.Entities
{
    public class QuantityUnit
    {
        public Guid Id { get; set; }

        public Guid QuantityId { get; set; }

        public Guid UnitId { get; set; }

        public virtual Quantity Quantity { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual ICollection<CharacteristicQuantityUnit> CharacteristicQuantityUnits { get; set; }
    }
}
