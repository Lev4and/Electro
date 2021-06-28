using Electro.Model.Database.Entities;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IQuantitiesRepository
    {
        bool ContainsQuantityByName(string name);

        bool SaveQuantity(Quantity entity);

        Quantity GetQuantityById(Guid id, bool track = false);

        IQueryable<Quantity> GetQuantities(bool track = false);

        void DeleteQuantityById(Guid id);

        void Detach(Quantity entity);
    }
}
