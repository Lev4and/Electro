using System;

namespace Electro.Model.Database.Entities
{
    public class SectionCharacteristicCategory
    {
        public Guid Id { get; set; }

        public Guid SectionId { get; set; }

        public Guid CategoryId { get; set; }

        public SectionCharacteristic Section { get; set; }

        public Category Category { get; set; }
    }
}
