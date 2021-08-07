using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Product
{
    public interface IProductsSorter
    {
        SortingOption SortingOption { get; }

        IQueryable<Entities.Product> Sort(IQueryable<Entities.Product> products);
    }
}
