using System;
using System.Collections.Generic;

namespace Electro.Model.Database.Entities
{
    public class CharacteristicQuantityUnit
    {
        public Guid Id { get; set; }

        public Guid CharacteristicId { get; set; }

        public Guid QuantityUnitId { get; set; }

        public virtual Characteristic Characteristic { get; set; }

        public virtual QuantityUnit QuantityUnit { get; set; }

        public virtual ICollection<CharacteristicQuantityUnitValue> Values { get; set; }
    }
}
