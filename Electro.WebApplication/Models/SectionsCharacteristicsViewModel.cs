using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class SectionsCharacteristicsViewModel
    {
        public Pagination Pagination { get; set; }

        public SectionsCharacteristicsFilters Filters { get; set; }

        public List<int> Limits { get; set; }

        public List<SectionCharacteristic> SectionCharacteristics { get; set; }

        public Dictionary<SortingOption, string> SortingOptions { get; set; }
    }
}
