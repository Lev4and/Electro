using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Product
{
    public class ByAscendingNameProductsSorter : IProductsSorter
    {
        public SortingOption SortingOption { get; }

        public ByAscendingNameProductsSorter()
        {
            SortingOption = SortingOption.ByAscendingName;
        }

        public IQueryable<Entities.Product> Sort(IQueryable<Entities.Product> products)
        {
            return products.OrderBy(product => product.Model);
        }
    }
}
