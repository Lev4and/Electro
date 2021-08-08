using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Category
{
    public class ByDescendingNameCategoriesSorter : ICategoriesSorter
    {
        public SortingOption SortingOption { get; }

        public ByDescendingNameCategoriesSorter()
        {
            SortingOption = SortingOption.ByDescendingName;
        }

        public IQueryable<Entities.Category> Sort(IQueryable<Entities.Category> categories)
        {
            return categories.OrderByDescending(category => category.Name);
        }
    }
}
