using Electro.Model.Database.AnonymousTypes;
using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IManufacturersRepository
    {
        bool ContainsManufacturerByName(string name);

        bool SaveManufacturer(Manufacturer entity);

        int GetCountManufacturers(ManufacturersFilters filters);

        Manufacturer GetManufacturerById(Guid id, bool track = false);

        Manufacturer GetManufacturerByName(string name, bool track = false);

        IQueryable<Manufacturer> GetManufacturers(bool track = false);

        IQueryable<Manufacturer> GetManufacturers(string searchString, int itemsPerResult, bool track = false);

        IQueryable<Manufacturer> GetManufacturers(ManufacturersFilters filters, bool track = false);

        IQueryable<ProductsManufacturer> GetManufacturersByCategoryId(Guid categoryId, bool track = false);

        void DeleteManufacturerById(Guid id);
    }
}
