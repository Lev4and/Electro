using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFProductMainPhotosRepository : IProductMainPhotosRepository
    {
        private readonly ElectroDbContext _context;

        public EFProductMainPhotosRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsProductMainPhotoByProductId(Guid productId)
        {
            return _context.ProductMainPhotos
                .SingleOrDefault(productMainPhoto => productMainPhoto.ProductId == productId) != null;
        }

        public bool SaveProductMainPhoto(ProductMainPhoto entity)
        {
            if(entity.Id == default)
            {
                if (!ContainsProductMainPhotoByProductId(entity.ProductId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetProductMainPhotoById(entity.Id);

                if(oldVersionEntity.ProductId != entity.ProductId)
                {
                    if (!ContainsProductMainPhotoByProductId(entity.ProductId))
                    {
                        _context.Entry(entity).State = EntityState.Modified;
                        _context.SaveChanges();

                        return true;
                    }
                }
                else
                {
                    _context.Entry(entity).State = EntityState.Modified;
                    _context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public ProductMainPhoto GetProductMainPhotoById(Guid id, bool track = false)
        {
            IQueryable<ProductMainPhoto> productMainPhotos = _context.ProductMainPhotos;

            if (!track)
            {
                productMainPhotos = productMainPhotos.AsNoTracking();
            }

            return productMainPhotos.SingleOrDefault(productMainPhoto => productMainPhoto.Id == id);
        }

        public ProductMainPhoto GetProductMainPhotoByProductId(Guid productId, bool track = false)
        {
            IQueryable<ProductMainPhoto> productMainPhotos = _context.ProductMainPhotos;

            if (!track)
            {
                productMainPhotos = productMainPhotos.AsNoTracking();
            }

            return productMainPhotos.SingleOrDefault(productMainPhoto => productMainPhoto.ProductId == productId);
        }

        public void DeleteProductMainPhotoById(Guid id)
        {
            _context.ProductMainPhotos.Remove(GetProductMainPhotoById(id));
            _context.SaveChanges();
        }
    }
}
