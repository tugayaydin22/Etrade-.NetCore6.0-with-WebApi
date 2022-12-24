using Etrade.Entities.Models.Identity;
using Etrade.Entities.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Etrade.UI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            var users=new List<AppUser>();
            foreach (var item in admins)
            {
                users =_userManager.Users.Where(i => i.Id != item.Id).ToList();
            }
          
            return View(users);
        }
        public async Task<IActionResult> RoleAssign(int id)
        {
            var user=await _userManager.FindByIdAsync(id.ToString());
            var roles = _roleManager.Roles.Where(i => i.Name != "Admin").ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            var RoleAssigns =new List<RoleAssignViewModel>();
            roles.ForEach(role => RoleAssigns.Add(new RoleAssignViewModel()
            {
                HasAssign = userRoles.Contains(role.Name),
                Id = role.Id,
                Name = role.Name

            }));
            ViewBag.Username = user.Name;
            return View(RoleAssigns);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(List<RoleAssignViewModel> models, int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            foreach (var role in models)
            {
                if (role.HasAssign)
                    await _userManager.AddToRoleAsync(user, role.Name);
                else
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return RedirectToAction("Index");

            return NotFound("Silme işlemi başarısız");
        }
    }
}
