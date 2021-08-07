using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Product
{
    public class ByPopularityProductsSorter : IProductsSorter
    {
        public SortingOption SortingOption { get; }

        public ByPopularityProductsSorter()
        {
            SortingOption = SortingOption.ByPopularity;
        }

        public IQueryable<Entities.Product> Sort(IQueryable<Entities.Product> products)
        {
            return products;
        }
    }
}
