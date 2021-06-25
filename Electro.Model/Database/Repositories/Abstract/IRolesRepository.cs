using Electro.Model.Database.Entities;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IRolesRepository
    {
        ApplicatonRole GetRoleByName(string name, bool track = false);

        IQueryable<ApplicatonRole> GetRoles(bool track = false);
    }
}
