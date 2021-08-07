using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Product
{
    public class ByAscendingPriceProductsSorter : IProductsSorter
    {
        public SortingOption SortingOption { get; }

        public ByAscendingPriceProductsSorter()
        {
            SortingOption = SortingOption.ByAscendingPrice;
        }

        public IQueryable<Entities.Product> Sort(IQueryable<Entities.Product> products)
        {
            return products.OrderBy(product => product.Price);
        }
    }
}
