using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IUsersRepository
    {
        bool ContainsUserByName(string name);

        bool ContainsUserByEmail(string email);

        bool ContainsUserByUserIdAndUserName(string userId, string userName);

        bool ContainsUserByUserIdAndEmail(string userId, string email);

        bool SaveUser(IdentityUser entity, IdentityRole role, string currentPassword, string newPassword);

        IdentityUser GetUserById(Guid id, bool track = false);

        IQueryable<IdentityUser> GetManagers(bool track = false);

        void DeleteManagerById(Guid id);
    }
}
