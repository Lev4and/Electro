using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Product
{
    public class DefaultProductsSorter : IProductsSorter
    {
        public SortingOption SortingOption { get; }

        public DefaultProductsSorter()
        {
            SortingOption = SortingOption.Default;
        }

        public IQueryable<Entities.Product> Sort(IQueryable<Entities.Product> products)
        {
            return products.OrderBy(product => product.Id);
        }
    }
}
