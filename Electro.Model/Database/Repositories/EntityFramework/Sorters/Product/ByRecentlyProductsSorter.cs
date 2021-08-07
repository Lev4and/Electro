using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Product
{
    public class ByRecentlyProductsSorter : IProductsSorter
    {
        public SortingOption SortingOption { get; }

        public ByRecentlyProductsSorter()
        {
            SortingOption = SortingOption.ByRecently;
        }

        public IQueryable<Entities.Product> Sort(IQueryable<Entities.Product> products)
        {
            return products.OrderByDescending(product => product.CreatedAt);
        }
    }
}
