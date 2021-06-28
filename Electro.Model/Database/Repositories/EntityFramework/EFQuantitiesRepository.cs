using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFQuantitiesRepository : IQuantitiesRepository
    {
        private readonly ElectroDbContext _context;

        public EFQuantitiesRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsQuantityByName(string name)
        {
            return _context.Quantities.SingleOrDefault(quantity => quantity.Name == name) != null;
        }

        public bool SaveQuantity(Quantity entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsQuantityByName(entity.Name))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetQuantityById(entity.Id);

                if (oldVersionEntity.Name != entity.Name)
                {
                    if (!ContainsQuantityByName(entity.Name))
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

        public Quantity GetQuantityById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.Quantities
                    .Include(quantity => quantity.QuantityUnits).ThenInclude(quantityUnit => quantityUnit.Unit)
                    .SingleOrDefault(quantity => quantity.Id == id);
            }
            else
            {
                return _context.Quantities
                    .Include(quantity => quantity.QuantityUnits).ThenInclude(quantityUnit => quantityUnit.Unit)
                    .AsNoTracking()
                    .SingleOrDefault(quantity => quantity.Id == id);
            }
        }

        public IQueryable<Quantity> GetQuantities(bool track = false)
        {
            if (track)
            {
                return _context.Quantities
                    .Include(quantity => quantity.QuantityUnits).ThenInclude(quantityUnit => quantityUnit.Unit);
            }
            else
            {
                return _context.Quantities
                    .Include(quantity => quantity.QuantityUnits).ThenInclude(quantityUnit => quantityUnit.Unit)
                    .AsNoTracking();
            }
        }


        public void DeleteQuantityById(Guid id)
        {
            _context.Quantities.Remove(GetQuantityById(id));
            _context.SaveChanges();
        }

        public void Detach(Quantity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
