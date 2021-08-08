using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Product
{
    public class ByAncientProductsSorter : IProductsSorter
    {
        public SortingOption SortingOption { get; }

        public ByAncientProductsSorter()
        {
            SortingOption = SortingOption.ByAncient;
        }

        public IQueryable<Entities.Product> Sort(IQueryable<Entities.Product> products)
        {
            return products.OrderBy(product => product.CreatedAt);
        }
    }
}
