using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Product
{
    public class ByAverageRatingProductsSorter : IProductsSorter
    {
        public SortingOption SortingOption { get; }

        public ByAverageRatingProductsSorter()
        {
            SortingOption = SortingOption.ByAverageRating;
        }

        public IQueryable<Entities.Product> Sort(IQueryable<Entities.Product> products)
        {
            return products;
        }
    }
}
