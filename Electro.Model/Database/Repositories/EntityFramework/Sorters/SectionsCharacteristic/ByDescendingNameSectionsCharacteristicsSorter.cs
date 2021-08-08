using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.SectionsCharacteristic
{
    public class ByDescendingNameSectionsCharacteristicsSorter : ISectionsCharacteristicsSorter
    {
        public SortingOption SortingOption { get; }

        public ByDescendingNameSectionsCharacteristicsSorter()
        {
            SortingOption = SortingOption.ByDescendingName;
        }

        public IQueryable<SectionCharacteristic> Sort(IQueryable<SectionCharacteristic> sectionCharacteristics)
        {
            return sectionCharacteristics.OrderByDescending(sectionCharacteristic => sectionCharacteristic.Name);
        }
    }
}
