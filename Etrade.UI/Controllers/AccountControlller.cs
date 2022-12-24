using Etrade.Entities.Models.Identity;
using Etrade.Entities.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Etrade.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Login()
        {
            if (User.Identity.Name != null)
                return RedirectToAction("Index", "Home");//adres çubuğuna account/logine gidemesin diye
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.Rememberme, true);
                if (result.Succeeded)
                {
                    
                    return RedirectToAction("Index", "Home");
                }
                   
            }

            return View(login);
        }
        public IActionResult Register()
        {
            if (User.Identity.Name != null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            var user = new AppUser()
            {
                Name = register.Name,
                Surname = register.Surname,
                UserName = register.Username,
                Email = register.Email
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            
            if (result.Succeeded)
            {
                var result2 = await _userManager.AddToRoleAsync(user, "Users");

                return RedirectToAction("Login");

            }


            return View(register);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
