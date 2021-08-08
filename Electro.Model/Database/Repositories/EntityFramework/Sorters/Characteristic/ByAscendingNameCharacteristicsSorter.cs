using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Characteristic
{
    public class ByAscendingNameCharacteristicsSorter : ICharacteristicsSorter
    {
        public SortingOption SortingOption { get; }

        public ByAscendingNameCharacteristicsSorter()
        {
            SortingOption = SortingOption.ByAscendingName;
        }

        public IQueryable<Entities.Characteristic> Sort(IQueryable<Entities.Characteristic> characteristics)
        {
            return characteristics.OrderBy(characteristic => characteristic.Name);
        }
    }
}
