using System;

namespace Electro.Model.Database.AuxiliaryTypes
{
    public class CategoriesFilters
    {
        public int NumberPage { get; set; }

        public int ItemsPerPage { get; set; }

        public string SearchString { get; set; }

        public Guid? ParentCategoryId { get; set; }
        
        public SortingOption SortingOption { get; set; }
    }
}
