using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Category Parent { get; set; }

        public virtual CategoryPhoto Photo { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Category> Children { get; set; }

        public virtual ICollection<CharacteristicCategory> Characteristics { get; set; }

        public virtual ICollection<SectionCharacteristicCategory> SectionsCharacteristics { get; set; }

        public Category()
        {

        }

        public Category(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
