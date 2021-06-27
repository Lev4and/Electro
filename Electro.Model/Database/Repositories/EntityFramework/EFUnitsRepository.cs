using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFUnitsRepository : IUnitsRepository
    {
        private readonly ElectroDbContext _context;

        public EFUnitsRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsUnitByName(string name)
        {
            return _context.Units.SingleOrDefault(unit => unit.Name == name) != null;
        }

        public bool SaveUnit(Unit entity)
        {
            if(entity.Id == default)
            {
                if (!ContainsUnitByName(entity.Name))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetUnitById(entity.Id);

                if(oldVersionEntity.Name != entity.Name)
                {
                    if (!ContainsUnitByName(entity.Name))
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

        public Unit GetUnitById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.Units
                    .Include(unit => unit.QuantityUnits).ThenInclude(quantityUnit => quantityUnit.Quantity)
                    .SingleOrDefault(unit => unit.Id == id);
            }
            else
            {
                return _context.Units
                    .Include(unit => unit.QuantityUnits).ThenInclude(quantityUnit => quantityUnit.Quantity)
                    .AsNoTracking()
                    .SingleOrDefault(unit => unit.Id == id);
            }
        }

        public IQueryable<Unit> GetUnits(bool track = false)
        {
            if (track)
            {
                return _context.Units
                    .Include(unit => unit.QuantityUnits).ThenInclude(quantityUnit => quantityUnit.Quantity);
            }
            else
            {
                return _context.Units
                    .Include(unit => unit.QuantityUnits).ThenInclude(quantityUnit => quantityUnit.Quantity)
                    .AsNoTracking();
            }
        }

        public void DeleteUnitById(Guid id)
        {
            _context.Units.Remove(GetUnitById(id));
            _context.SaveChanges();
        }
    }
}
