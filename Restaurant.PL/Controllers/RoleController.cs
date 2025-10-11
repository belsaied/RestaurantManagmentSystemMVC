using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.PL.ViewModels.Auth_ViewModels;

namespace Restaurant.PL.Controllers
{
    public class RoleController(RoleManager<IdentityRole>_roleManager) : Controller
    {
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
                    return View();
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
