using Electro.Model.Database.Entities;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IManufacturersRepository
    {
        bool ContainsManufacturerByName(string name);

        bool SaveManufacturer(Manufacturer entity);

        Manufacturer GetManufacturerById(Guid id, bool track = false);

        IQueryable<Manufacturer> GetManufacturers(bool track = false);

        void DeleteManufacturerById(Guid id);
    }
}
