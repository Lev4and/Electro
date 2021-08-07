using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Product
{
    public class ByDescendingPriceProductsSorter : IProductsSorter
    {
        public SortingOption SortingOption { get; }

        public ByDescendingPriceProductsSorter()
        {
            SortingOption = SortingOption.ByDescendingPrice;
        }

        public IQueryable<Entities.Product> Sort(IQueryable<Entities.Product> products)
        {
            return products.OrderByDescending(product => product.Price);
        }
    }
}
