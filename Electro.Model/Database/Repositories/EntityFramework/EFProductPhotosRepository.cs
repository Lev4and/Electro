using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFProductPhotosRepository : IProductPhotosRepository
    {
        private readonly ElectroDbContext _context;

        public EFProductPhotosRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsProductPhotoByProductIdAndUrl(Guid productId, string url)
        {
            return _context.ProductPhotos
                .SingleOrDefault(productPhoto => productPhoto.ProductId == productId && productPhoto.Url == url) != null;
        }

        public bool SaveProductPhoto(ProductPhoto entity)
        {
            if(entity.Id == default)
            {
                if(!ContainsProductPhotoByProductIdAndUrl(entity.ProductId, entity.Url))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetProductPhotoById(entity.Id);

                if(oldVersionEntity.ProductId != entity.ProductId || oldVersionEntity.Url != entity.Url)
                {
                    if (!ContainsProductPhotoByProductIdAndUrl(entity.ProductId, entity.Url))
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

        public ProductPhoto GetProductPhotoById(Guid id, bool track = false)
        {
            IQueryable<ProductPhoto> productPhotos = _context.ProductPhotos;

            if (!track)
            {
                productPhotos = productPhotos.AsNoTracking();
            }

            return productPhotos.SingleOrDefault(productPhoto => productPhoto.Id == id);
        }

        public IQueryable<ProductPhoto> GetProductPhotosByProductId(Guid productId, bool track = false)
        {
            IQueryable<ProductPhoto> productPhotos = _context.ProductPhotos;

            if (!track)
            {
                productPhotos = productPhotos.AsNoTracking();
            }

            return productPhotos.Where(productPhoto => productPhoto.ProductId == productId);
        }

        public IQueryable<ProductPhoto> GetNotIncludedInTheListProductPhotosByProductId(Guid productId, List<ProductPhoto> productPhotos, bool track = false)
        {
            IQueryable<ProductPhoto> photos = _context.ProductPhotos;

            if (!track)
            {
                photos = photos.AsNoTracking();
            }

            return photos.Where(productPhoto => productPhoto.ProductId == productId &&
                    productPhotos.Contains(productPhoto) == false);
        }

        public void DeleteProductPhotoById(Guid id)
        {
            _context.ProductPhotos.Remove(GetProductPhotoById(id));
            _context.SaveChanges();
        }

        public void DeleteRangeProductPhotos(List<ProductPhoto> productPhotos)
        {
            _context.ProductPhotos.RemoveRange(productPhotos);
            _context.SaveChanges();
        }
    }
}
