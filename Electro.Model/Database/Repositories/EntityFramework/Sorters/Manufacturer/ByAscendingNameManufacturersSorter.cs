using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Manufacturer
{
    public class ByAscendingNameManufacturersSorter : IManufacturersSorter
    {
        public SortingOption SortingOption { get; }

        public ByAscendingNameManufacturersSorter()
        {
            SortingOption = SortingOption.ByAscendingName;
        }

        public IQueryable<Entities.Manufacturer> Sort(IQueryable<Entities.Manufacturer> manufacturers)
        {
            return manufacturers.OrderBy(manufacturer => manufacturer.Name);
        }
    }
}
