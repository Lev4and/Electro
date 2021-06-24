using Electro.Model.Database.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFUsersRepository : IUsersRepository
    {
        private readonly ElectroDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EFUsersRepository(ElectroDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public bool ContainsUserByName(string name)
        {
            return _userManager.Users.AsNoTracking().SingleOrDefault(user => user.UserName == name) != null;
        }

        public bool ContainsUserByEmail(string email)
        {
            return _userManager.Users.AsNoTracking().SingleOrDefault(user => user.Email == email) != null;
        }

        public bool ContainsUserByUserIdAndUserName(string userId, string userName)
        {
            return _userManager.Users.AsNoTracking().SingleOrDefault(user => user.Id == userId && user.UserName == userName) != null;
        }

        public bool ContainsUserByUserIdAndEmail(string userId, string email)
        {
            return _userManager.Users.AsNoTracking().SingleOrDefault(user => user.Id == userId && user.Email == email) != null;
        }

        public bool SaveUser(IdentityUser entity, IdentityRole role, string currentPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                if (!ContainsUserByName(entity.UserName))
                {
                    if (_userManager.CreateAsync(entity, currentPassword).Result == IdentityResult.Success)
                    {
                        if (_userManager.AddToRoleAsync(entity, role.Name).Result == IdentityResult.Success)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
            else
            {
                if (!ContainsUserByUserIdAndUserName(entity.Id, entity.UserName) || !ContainsUserByUserIdAndEmail(entity.Id, entity.Email))
                {
                    if (!ContainsUserByUserIdAndUserName(entity.Id, entity.UserName) ? !ContainsUserByName(entity.UserName) : !ContainsUserByEmail(entity.Email))
                    {
                        if (_userManager.UpdateAsync(entity).Result == IdentityResult.Success)
                        {
                            if (_userManager.RemoveFromRolesAsync(entity, _userManager.GetRolesAsync(entity).Result).Result == IdentityResult.Success)
                            {
                                if (_userManager.AddToRoleAsync(entity, role.Name).Result == IdentityResult.Success)
                                {
                                    if (!string.IsNullOrEmpty(currentPassword) && !string.IsNullOrEmpty(newPassword))
                                    {
                                        if (_userManager.ChangePasswordAsync(entity, currentPassword, newPassword).Result == IdentityResult.Success)
                                        {

                                        }
                                    }

                                    return true;
                                }
                            }
                        }

                        return false;
                    }
                }
                else
                {
                    if (_userManager.UpdateAsync(entity).Result == IdentityResult.Success)
                    {
                        if (_userManager.RemoveFromRolesAsync(entity, _userManager.GetRolesAsync(entity).Result).Result == IdentityResult.Success)
                        {
                            if (_userManager.AddToRoleAsync(entity, role.Name).Result == IdentityResult.Success)
                            {
                                if (!string.IsNullOrEmpty(currentPassword) && !string.IsNullOrEmpty(newPassword))
                                {
                                    if (_userManager.ChangePasswordAsync(entity, currentPassword, newPassword).Result == IdentityResult.Success)
                                    {

                                    }
                                }

                                return true;
                            }
                        }
                    }

                    return false;
                }
            }

            return false;
        }

        public IdentityUser GetUserById(Guid id, bool track = false)
        {
            if (track)
            {
                return _userManager.Users.SingleOrDefault(user => user.Id == id.ToString("D"));
            }
            else
            {
                return _userManager.Users.AsNoTracking().SingleOrDefault(user => user.Id == id.ToString("D"));
            }
        }

        public IQueryable<IdentityUser> GetManagers(bool track = false)
        {
            if (track)
            {
                return _userManager.GetUsersInRoleAsync("Менеджер").Result.AsQueryable();
            }
            else
            {
                return _userManager.GetUsersInRoleAsync("Менеджер").Result.AsQueryable().AsNoTracking();
            }
        }

        public void DeleteManagerById(Guid id)
        {
            _userManager.DeleteAsync(GetUserById(id)).Wait();
        }
    }
}
