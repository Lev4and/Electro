using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface ICharacteristicsRepository
    {
        bool ContainsCharacteristicByName(string name);

        bool SaveCharacteristic(Characteristic entity);

        int GetCountCharacteristics(CharacteristicsFilters filters);

        Characteristic GetCharacteristicById(Guid id, bool track = false);

        Characteristic GetCharacteristicByName(string name, bool track = false);

        IQueryable<Characteristic> GetCharacteristics(bool track = false);

        IQueryable<Characteristic> GetCharacteristics(CharacteristicsFilters filters, bool track = false);

        IQueryable<Characteristic> GetCharacteristicsByCategoryId(Guid categoryId, bool track = false);

        IQueryable<Characteristic> GetNotUsedCharacteristicsForProductByProductIdAndCategoryId(Guid productId, Guid categoryId,
            bool track = false);

        void DeleteCharacteristicById(Guid id);
    }
}
