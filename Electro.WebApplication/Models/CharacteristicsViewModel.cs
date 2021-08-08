using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class CharacteristicsViewModel
    {
        public Pagination Pagination { get; set; }

        public CharacteristicsFilters Filters { get; set; }

        public List<int> Limits { get; set; }

        public List<Characteristic> Characteristics { get; set; }

        public Dictionary<SortingOption, string> SortingOptions { get; set; }
    }
}
