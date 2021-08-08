using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Characteristic
{
    public class ByDescendingNameCharacteristicsSorter : ICharacteristicsSorter
    {
        public SortingOption SortingOption { get; }

        public ByDescendingNameCharacteristicsSorter()
        {
            SortingOption = SortingOption.ByDescendingName;
        }

        public IQueryable<Entities.Characteristic> Sort(IQueryable<Entities.Characteristic> characteristics)
        {
            return characteristics.OrderByDescending(characteristic => characteristic.Name);
        }
    }
}
