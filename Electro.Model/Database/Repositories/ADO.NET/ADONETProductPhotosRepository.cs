using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.ADONET
{
    public class ADONETProductPhotosRepository : IProductPhotosRepository
    {
        private readonly ElectroDbContext _context;

        public ADONETProductPhotosRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsProductPhotoByProductIdAndUrl(Guid productId, string url)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM ProductPhotos " +
                $"WHERE ProductPhotos.ProductId = '{productId}' AND ProductPhotos.Url = '{url}'";

            return _context.ExecuteQuery(query).Rows.Count > 0;
        }

        public bool SaveProductPhoto(ProductPhoto entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsProductPhotoByProductIdAndUrl(entity.ProductId, entity.Url))
                {
                    var id = Guid.NewGuid();
                    var query = $"INSERT INTO [ProductPhotos] (Id, ProductId, IsAbsolute, Url) VALUES ('{id}', " +
                        $"'{entity.ProductId}', 1, '{entity.Url}')";

                    entity.Id = id;

                    _context.ExecuteQuery(query);

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetProductPhotoById(entity.Id);

                if (oldVersionEntity.ProductId != entity.ProductId || oldVersionEntity.Url != entity.Url)
                {
                    if (!ContainsProductPhotoByProductIdAndUrl(entity.ProductId, entity.Url))
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

        public ProductPhoto GetProductPhotoById(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductPhoto> GetProductPhotosByProductId(Guid productId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductPhoto> GetNotIncludedInTheListProductPhotosByProductId(Guid productId, List<ProductPhoto> productPhotos, bool track = false)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductPhotoById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRangeProductPhotos(List<ProductPhoto> productPhotos)
        {
            throw new NotImplementedException();
        }
    }
}
