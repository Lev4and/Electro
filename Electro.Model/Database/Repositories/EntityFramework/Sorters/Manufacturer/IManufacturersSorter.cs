using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Manufacturer
{
    public interface IManufacturersSorter
    {
        SortingOption SortingOption { get; }

        IQueryable<Entities.Manufacturer> Sort(IQueryable<Entities.Manufacturer> manufacturers);
    }
}
