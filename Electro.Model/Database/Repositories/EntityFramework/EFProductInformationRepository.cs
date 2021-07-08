using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFProductInformationRepository : IProductInformationRepository
    {
        private readonly ElectroDbContext _context;

        public EFProductInformationRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsProductInformationByProductId(Guid productId, bool track = false)
        {
            return _context.ProductInformation
                .SingleOrDefault(productInformation => productInformation.ProductId == productId) != null;
        }

        public bool SaveProductInformation(ProductInformation entity)
        {
            if(entity.Id == default)
            {
                if (!ContainsProductInformationByProductId(entity.ProductId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetProductInformationById(entity.Id);

                if(oldVersionEntity.ProductId != entity.ProductId)
                {
                    if (!ContainsProductInformationByProductId(entity.ProductId))
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

        public ProductInformation GetProductInformationById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.ProductInformation
                    .SingleOrDefault(productInformation => productInformation.Id == id);
            }
            else
            {
                return _context.ProductInformation
                    .AsNoTracking()
                    .SingleOrDefault(productInformation => productInformation.Id == id);
            }
        }

        public ProductInformation GetProductInformationByProductId(Guid productId, bool track = false)
        {
            if (track)
            {
                return _context.ProductInformation
                    .SingleOrDefault(productInformation => productInformation.ProductId == productId);
            }
            else
            {
                return _context.ProductInformation
                    .AsNoTracking()
                    .SingleOrDefault(productInformation => productInformation.ProductId == productId);
            }
        }

        public void DeleteProductInformationById(Guid id)
        {
            _context.ProductInformation.Remove(GetProductInformationById(id));
            _context.SaveChanges();
        }
    }
}
