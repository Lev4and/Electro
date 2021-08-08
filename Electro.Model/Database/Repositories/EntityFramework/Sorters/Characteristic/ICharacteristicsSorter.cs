using Electro.Model.Database.AuxiliaryTypes;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework.Sorters.Characteristic
{
    public interface ICharacteristicsSorter
    {
        SortingOption SortingOption { get; }

        IQueryable<Entities.Characteristic> Sort(IQueryable<Entities.Characteristic> characteristics);
    }
}
