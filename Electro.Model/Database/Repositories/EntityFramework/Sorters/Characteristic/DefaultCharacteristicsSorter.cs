using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Characteristic
{
    public class DefaultCharacteristicsSorter : ICharacteristicsSorter
    {
        public SortingOption SortingOption { get; }

        public DefaultCharacteristicsSorter()
        {
            SortingOption = SortingOption.Default;
        }

        public IQueryable<Entities.Characteristic> Sort(IQueryable<Entities.Characteristic> characteristics)
        {
            return characteristics.OrderBy(characteristic => characteristic.Id);
        }
    }
}
