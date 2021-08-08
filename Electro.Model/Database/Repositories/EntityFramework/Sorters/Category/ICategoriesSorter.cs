using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Category
{
    public interface ICategoriesSorter
    {
        SortingOption SortingOption { get; }

        IQueryable<Entities.Category> Sort(IQueryable<Entities.Category> categories);
    }
}
