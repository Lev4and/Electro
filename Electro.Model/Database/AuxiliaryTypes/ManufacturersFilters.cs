namespace Electro.Model.Database.AuxiliaryTypes
{
    public class ManufacturersFilters
    {
        public int NumberPage { get; set; }

        public int ItemsPerPage { get; set; }

        public string SearchString { get; set; }

        public SortingOption SortingOption { get; set; }
    }
}
