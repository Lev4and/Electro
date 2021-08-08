using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Category
{
    public class ByAscendingNameCategoriesSorter : ICategoriesSorter
    {
        public SortingOption SortingOption { get; }

        public ByAscendingNameCategoriesSorter()
        {
            SortingOption = SortingOption.ByAscendingName;
        }

        public IQueryable<Entities.Category> Sort(IQueryable<Entities.Category> categories)
        {
            return categories.OrderBy(category => category.Name);
        }
    }
}
