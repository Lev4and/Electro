using System;
using System.Collections.Generic;

namespace Electro.Model.Database.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public Category Parent { get; set; }

        public CategoryPhoto Photo { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Category> Children { get; set; }

        public virtual ICollection<CharacteristicCategory> Characteristics { get; set; }

        public virtual ICollection<SectionCharacteristicCategory> SectionsCharacteristics { get; set; }
    }
}
