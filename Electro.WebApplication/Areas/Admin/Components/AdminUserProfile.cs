using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Electro.WebApplication.Areas.Admin.Components
{
    public class AdminUserProfile : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminUserProfile(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name);
            var role = _userManager.GetRolesAsync(user.Result);

            var viewModel = new AdminUserProfileViewModel()
            {
                Name = user.Result.UserName,
                Role = role.Result.First()
            };

            return View("Default", viewModel);
        }
    }
}
