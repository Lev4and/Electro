using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Product
{
    public class ByDescendingNameProductsSorter : IProductsSorter
    {
        public SortingOption SortingOption { get; }

        public ByDescendingNameProductsSorter()
        {
            SortingOption = SortingOption.ByDescendingName;
        }

        public IQueryable<Entities.Product> Sort(IQueryable<Entities.Product> products)
        {
            return products.OrderByDescending(product => product.Model);
        }
    }
}
