using Electro.Model.Database;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Electro.WebApplication.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(DataManager dataManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _dataManager = dataManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userEmail = await _userManager.FindByEmailAsync(model.EmailOrLogin);
                var userName = await _userManager.FindByNameAsync(model.EmailOrLogin);

                if (userEmail != null || userName != null)
                {
                    var user = userEmail != null ? userEmail : userName;

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Неверный пароль");
                    }
                }
                else
                {
                    if (userEmail == null && userName == null)
                    {
                        ModelState.AddModelError("EmailOrLogin", "Введен неверный адрес электронной почты или логин");
                    }
                    else
                    {
                        if (userEmail == null)
                        {
                            ModelState.AddModelError("EmailOrLogin", "Введен неверный адрес электронной почты");
                        }
                        else
                        {
                            ModelState.AddModelError("EmailOrLogin", "Введен неверный логин");
                        }
                    }
                }
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
