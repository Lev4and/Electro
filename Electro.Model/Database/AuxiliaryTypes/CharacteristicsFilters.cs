namespace Electro.Model.Database.AuxiliaryTypes
{
    public class CharacteristicsFilters
    {
        public int NumberPage { get; set; }

        public int ItemsPerPage { get; set; }

        public string SearchString { get; set; }

        public SortingOption SortingOption { get; set; }
    }
}
