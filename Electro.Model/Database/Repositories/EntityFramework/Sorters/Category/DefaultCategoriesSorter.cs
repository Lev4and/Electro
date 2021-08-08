using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Category
{
    public class DefaultCategoriesSorter : ICategoriesSorter
    {
        public SortingOption SortingOption { get; }

        public DefaultCategoriesSorter()
        {
            SortingOption = SortingOption.Default;
        }

        public IQueryable<Entities.Category> Sort(IQueryable<Entities.Category> categories)
        {
            return categories.OrderBy(category => category.Id);
        }
    }
}
