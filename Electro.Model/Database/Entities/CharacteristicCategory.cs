using System;

namespace Electro.Model.Database.Entities
{
    public class CharacteristicCategory
    {
        public Guid Id { get; set; }

        public Guid CharacteristicId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SectionId { get; set; }

        public bool UseWhenFiltering { get; set; }

        public bool UseWhenDisplayingAsBasicInformation { get; set; }

        public virtual SectionCharacteristic Section { get; set; }

        public virtual Characteristic Characteristic { get; set; }

        public virtual Category Category { get; set; }
    }
}
