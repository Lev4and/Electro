using Electro.Model.Database.Entities;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public EFUsersRepository(ElectroDbContext context, UserManager<ApplicationUser> userManager)
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

        public bool ContainsUserByIdAndName(Guid id, string name)
        {
            return _userManager.Users.AsNoTracking().SingleOrDefault(user => user.Id == id && user.UserName == name) != null;
        }

        public bool ContainsUserByIdAndEmail(Guid id, string email)
        {
            return _userManager.Users.AsNoTracking().SingleOrDefault(user => user.Id == id && user.Email == email) != null;
        }

        public bool SaveUser(ApplicationUser entity, ApplicatonRole role, string currentPassword, string newPassword)
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
                if (!ContainsUserByIdAndName(entity.Id, entity.UserName) || !ContainsUserByIdAndEmail(entity.Id, entity.Email))
                {
                    if (!ContainsUserByIdAndName(entity.Id, entity.UserName) ? !ContainsUserByName(entity.UserName) : !ContainsUserByEmail(entity.Email))
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

        public ApplicationUser GetUserById(Guid id, bool track = false)
        {
            if (track)
            {
                return _userManager.Users.SingleOrDefault(user => user.Id == id);
            }
            else
            {
                return _userManager.Users.AsNoTracking().SingleOrDefault(user => user.Id == id);
            }
        }

        public IQueryable<ApplicationUser> GetManagers(bool track = false)
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
