using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Manufacturer
{
    public class ByDescendingNameManufacturersSorter : IManufacturersSorter
    {
        public SortingOption SortingOption { get; }

        public ByDescendingNameManufacturersSorter()
        {
            SortingOption = SortingOption.ByDescendingName;
        }

        public IQueryable<Entities.Manufacturer> Sort(IQueryable<Entities.Manufacturer> manufacturers)
        {
            return manufacturers.OrderByDescending(manufacturer => manufacturer.Name);
        }
    }
}
