using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFRolesRepository : IRolesRepository
    {
        private readonly ElectroDbContext _context;

        public EFRolesRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public ApplicatonRole GetRoleByName(string name, bool track = false)
        {
            IQueryable<ApplicatonRole> roles = _context.Roles;

            if (!track)
            {
                roles = roles.AsNoTracking();
            }

            return roles.SingleOrDefault(role => role.Name == name);
        }

        public IQueryable<ApplicatonRole> GetRoles(bool track = false)
        {
            IQueryable<ApplicatonRole> roles = _context.Roles;

            if (!track)
            {
                roles = roles.AsNoTracking();
            }

            return roles;
        }
    }
}
