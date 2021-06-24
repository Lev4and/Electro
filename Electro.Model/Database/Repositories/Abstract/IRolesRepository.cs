using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IRolesRepository
    {
        IdentityRole GetRoleByName(string name, bool track = false);

        IQueryable<IdentityRole> GetRoles(bool track = false);
    }
}
