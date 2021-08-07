using Electro.Model.Database.Entities;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface ISectionsCharacteristicsRepository
    {
        bool ContainsSectionCharacteristicByName(string name);

        bool SaveSectionCharacteristic(SectionCharacteristic entity);

        SectionCharacteristic GetSectionCharacteristicById(Guid id, bool track = false);

        SectionCharacteristic GetSectionCharacteristicByName(string name, bool track = false);

        IQueryable<SectionCharacteristic> GetSectionsCharacteristics(bool track = false);

        IQueryable<SectionCharacteristic> GetSectionsCharacteristicsByCategoryId(Guid categoryId, bool track = false);

        void DeleteSectionCharacteristicById(Guid id);

        void Detach(SectionCharacteristic entity);
    }
}
