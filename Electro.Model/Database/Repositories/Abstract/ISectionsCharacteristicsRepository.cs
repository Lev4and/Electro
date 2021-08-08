using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface ISectionsCharacteristicsRepository
    {
        bool ContainsSectionCharacteristicByName(string name);

        bool SaveSectionCharacteristic(SectionCharacteristic entity);

        int GetCountSectionsCharacteristics(SectionsCharacteristicsFilters filters);

        SectionCharacteristic GetSectionCharacteristicById(Guid id, bool track = false);

        SectionCharacteristic GetSectionCharacteristicByName(string name, bool track = false);

        IQueryable<SectionCharacteristic> GetSectionsCharacteristics(bool track = false);

        IQueryable<SectionCharacteristic> GetSectionsCharacteristics(SectionsCharacteristicsFilters filters, bool track = false);

        IQueryable<SectionCharacteristic> GetSectionsCharacteristicsByCategoryId(Guid categoryId, bool track = false);

        void DeleteSectionCharacteristicById(Guid id);

        void Detach(SectionCharacteristic entity);
    }
}
