using Electro.Model.Database.Entities;
using System;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IManufacturerLogosRepository
    {
        bool ContainsManufacturerLogoByManufacturerId(Guid manufacturerId);

        bool SaveManufacturerLogo(ManufacturerLogo entity);

        ManufacturerLogo GetManufacturerLogoById(Guid id, bool track = false);

        ManufacturerLogo GetManufacturerLogoByManufacturerId(Guid manufacturerId, bool track = false);

        void DeleteManufacturerLogoById(Guid id);
    }
}
