using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using System;

namespace Electro.Model.Database.Repositories.ADONET
{
    public class ADONETManufacturerLogosRepository : IManufacturerLogosRepository
    {
        private readonly ElectroDbContext _context;

        public ADONETManufacturerLogosRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsManufacturerLogoByManufacturerId(Guid manufacturerId)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM ManufacturerLogos " +
                $"WHERE ManufacturerLogos.ManufacturerId = '{manufacturerId}'";

            return _context.ExecuteQuery(query).Rows.Count > 0;
        }

        public bool SaveManufacturerLogo(ManufacturerLogo entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsManufacturerLogoByManufacturerId(entity.ManufacturerId))
                {
                    var id = Guid.NewGuid();
                    var query = $"INSERT INTO [ManufacturerLogos] (Id, ManufacturerId, IsAbsolute, Url) VALUES ('{id}', " +
                        $"'{entity.ManufacturerId}', 1, '{entity.Url}')";

                    entity.Id = id;

                    _context.ExecuteQuery(query);

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetManufacturerLogoById(entity.Id);

                if (oldVersionEntity.ManufacturerId != entity.ManufacturerId)
                {
                    if (!ContainsManufacturerLogoByManufacturerId(entity.ManufacturerId))
                    {
                        //TODO: Обновление не предусматривается

                        return true;
                    }
                }
                else
                {
                    //TODO: Обновление не предусматривается

                    return true;
                }
            }

            return false;
        }

        public ManufacturerLogo GetManufacturerLogoById(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public ManufacturerLogo GetManufacturerLogoByManufacturerId(Guid manufacturerId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public void DeleteManufacturerLogoById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
