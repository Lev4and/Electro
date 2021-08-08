using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.SectionsCharacteristic
{
    public class ByAscendingNameSectionsCharacteristicsSorter : ISectionsCharacteristicsSorter
    {
        public SortingOption SortingOption { get; }

        public ByAscendingNameSectionsCharacteristicsSorter()
        {
            SortingOption = SortingOption.ByAscendingName;
        }

        public IQueryable<SectionCharacteristic> Sort(IQueryable<SectionCharacteristic> sectionCharacteristics)
        {
            return sectionCharacteristics.OrderBy(sectionCharacteristic => sectionCharacteristic.Name);
        }
    }
}
