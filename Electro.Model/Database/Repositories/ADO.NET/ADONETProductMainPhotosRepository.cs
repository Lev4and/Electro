using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using System;

namespace Electro.Model.Database.Repositories.ADONET
{
    public class ADONETProductMainPhotosRepository : IProductMainPhotosRepository
    {
        private readonly ElectroDbContext _context;

        public ADONETProductMainPhotosRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsProductMainPhotoByProductId(Guid productId)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM ProductMainPhotos " +
                $"WHERE ProductMainPhotos.ProductId = '{productId}'";

            return _context.ExecuteQuery(query).Rows.Count > 0;
        }

        public bool SaveProductMainPhoto(ProductMainPhoto entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsProductMainPhotoByProductId(entity.ProductId))
                {
                    var id = Guid.NewGuid();
                    var query = $"INSERT INTO [ProductMainPhotos] (Id, ProductId, IsAbsolute, Url) VALUES ('{id}', " +
                        $"'{entity.ProductId}', 1, '{entity.Url}')";

                    entity.Id = id;

                    _context.ExecuteQuery(query);

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetProductMainPhotoById(entity.Id);

                if (oldVersionEntity.ProductId != entity.ProductId)
                {
                    if (!ContainsProductMainPhotoByProductId(entity.ProductId))
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

        public ProductMainPhoto GetProductMainPhotoById(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public ProductMainPhoto GetProductMainPhotoByProductId(Guid productId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductMainPhotoById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
