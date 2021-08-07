using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFCategoryPhotosRepository : ICategoryPhotosRepository
    {
        private readonly ElectroDbContext _context;

        public EFCategoryPhotosRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsCategoryPhotoByCategoryId(Guid categoryId)
        {
            return _context.CategoryPhotos.SingleOrDefault(categoryPhoto => categoryPhoto.CategoryId == categoryId) != null;
        }

        public bool SaveCategoryPhoto(CategoryPhoto entity)
        {
            if(entity.Id == default)
            {
                if (!ContainsCategoryPhotoByCategoryId(entity.CategoryId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetCategoryPhotoById(entity.Id);

                if(oldVersionEntity.CategoryId != entity.CategoryId)
                {
                    if (!ContainsCategoryPhotoByCategoryId(entity.CategoryId))
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

        public CategoryPhoto GetCategoryPhotoById(Guid id, bool track = false)
        {
            IQueryable<CategoryPhoto> categoryPhotos = _context.CategoryPhotos;

            if (!track)
            {
                categoryPhotos = categoryPhotos.AsNoTracking();
            }

            return categoryPhotos.SingleOrDefault(categoryPhoto => categoryPhoto.Id == id);
        }

        public void DeleteCategoryPhotoById(Guid id)
        {
            _context.CategoryPhotos.Remove(GetCategoryPhotoById(id));
            _context.SaveChanges();
        }
    }
}
