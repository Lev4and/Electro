using System;

namespace Electro.Model.Database.Entities
{
    public class SectionCharacteristicCategory
    {
        public Guid Id { get; set; }

        public Guid SectionId { get; set; }

        public Guid CategoryId { get; set; }

        public virtual SectionCharacteristic Section { get; set; }

        public virtual Category Category { get; set; }
    }
}
