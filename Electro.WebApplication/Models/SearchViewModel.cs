namespace Electro.WebApplication.Models
{
    public class SearchViewModel
    {
        public string SearchString { get; set; }

        public ProductsViewModel ProductsViewModel { get; set; }

        public CategoriesViewModel CategoriesViewModel { get; set; }

        public ManufacturersViewModel ManufacturersViewModel { get; set; }
    }
}
