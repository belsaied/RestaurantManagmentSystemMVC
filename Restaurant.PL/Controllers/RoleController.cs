using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.PL.ViewModels.Auth_ViewModels;

namespace Restaurant.PL.Controllers
{
    [Authorize]

    public class RoleController(RoleManager<IdentityRole>_roleManager) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> AddRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                //mapping 
                var role = new IdentityRole()
                {
                    Name = roleViewModel.RoleName
                };
                //Save Db
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    ViewBag.sucess = true;
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(roleViewModel);
        }

    }
}
