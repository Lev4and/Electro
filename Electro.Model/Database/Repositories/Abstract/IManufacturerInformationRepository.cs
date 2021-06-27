using Electro.Model.Database.Entities;
using System;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IManufacturerInformationRepository
    {
        bool ContainsManufacturerInformationByManufacturerId(Guid manufacturerId);

        bool SaveManufacturerInformation(ManufacturerInformation entity);

        ManufacturerInformation GetManufacturerInformationById(Guid id, bool track = false);

        ManufacturerInformation GetManufacturerInformationByManufacturerId(Guid manufacturerId, bool track = false);

        void DeleteManufacturerInformationById(Guid id);
    }
}
