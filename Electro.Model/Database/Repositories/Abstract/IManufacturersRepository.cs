using Electro.Model.Database.AnonymousTypes;
using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IManufacturersRepository
    {
        bool ContainsManufacturerByName(string name);

        bool SaveManufacturer(Manufacturer entity);

        Manufacturer GetManufacturerById(Guid id, bool track = false);

        Manufacturer GetManufacturerByName(string name, bool track = false);

        IQueryable<Manufacturer> GetManufacturers(bool track = false);

        IQueryable<ProductsManufacturer> GetManufacturersByCategoryId(Guid categoryId, bool track = false);

        void DeleteManufacturerById(Guid id);
    }
}
