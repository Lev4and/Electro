using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Manufacturer
{
    public class DefaultManufacturersSorter : IManufacturersSorter
    {
        public SortingOption SortingOption { get; }

        public DefaultManufacturersSorter()
        {
            SortingOption = SortingOption.Default;
        }

        public IQueryable<Entities.Manufacturer> Sort(IQueryable<Entities.Manufacturer> manufacturers)
        {
            return manufacturers.OrderBy(manufacturer => manufacturer.Id);
        }
    }
}
