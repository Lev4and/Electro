using System.Collections.Generic;

namespace Electro.Model.Database.AuxiliaryTypes
{
    public class CatalogProductsFilters
    {
        public int NumberPage { get; set; }

        public int ItemsPerPage { get; set; }

        public Price RangePrice { get; set; }

        public SortingOption SortingOption { get; set; }

        public List<ManufacturerFilter> ManufacturerFilters { get; set; }

        public List<CharacteristicFilter> CharacteristicFilters { get; set; }
    }
}
