using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IQuantityUnitsRepository
    {
        bool ContainsQuantityUnitByQuantityIdAndUnitId(Guid quantityId, Guid unitId);

        bool SaveQuantityUnit(QuantityUnit entity);

        QuantityUnit GetQuantityUnitById(Guid id, bool track = false);

        IQueryable<QuantityUnit> GetQuantityUnits(bool track = false);

        void DeleteQuantityUnitById(Guid id);

        void DeleteQuantityUnit(QuantityUnit entity);

        void DeleteRangeQuantityUnits(List<QuantityUnit> quantityUnits);

        void Detach(QuantityUnit entity);
    }
}
