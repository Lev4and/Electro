using Electro.Model.Database.Entities;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IUnitsRepository
    {
        bool ContainsUnitByName(string name);

        bool SaveUnit(Unit entity);

        Unit GetUnitById(Guid id, bool track = false);

        IQueryable<Unit> GetUnits(bool track = false);

        IQueryable<Unit> GetnNotUsedUnitsForQuantityByQuantityId(Guid quantityId, bool track = false);

        void DeleteUnitById(Guid id);
    }
}
