using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.SectionsCharacteristic
{
    public class DefaultSectionsCharacteristicsSorter : ISectionsCharacteristicsSorter
    {
        public SortingOption SortingOption { get; }

        public DefaultSectionsCharacteristicsSorter()
        {
            SortingOption = SortingOption.Default;
        }

        public IQueryable<SectionCharacteristic> Sort(IQueryable<SectionCharacteristic> sectionCharacteristics)
        {
            return sectionCharacteristics.OrderBy(sectionCharacteristic => sectionCharacteristic.Id);
        }
    }
}
