using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.SectionsCharacteristic
{
    public interface ISectionsCharacteristicsSorter
    {
        SortingOption SortingOption { get; }

        IQueryable<Entities.SectionCharacteristic> Sort(IQueryable<Entities.SectionCharacteristic> sectionCharacteristics);
    }
}
