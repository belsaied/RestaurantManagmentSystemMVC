using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Restaurant.DAL.Models;
using Restaurant.PL.ViewModels.Auth_ViewModels;

namespace Restaurant.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager
        ,SignInManager<ApplicationUser>_signInManager) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            if (ModelState.IsValid)
            {
                //Mapping
                var appUser = new ApplicationUser()
                {
                    UserName= registerUserViewModel.UserName,
                    Email = registerUserViewModel.Email,
                    Address = registerUserViewModel.Address,
                    PhoneNumber=registerUserViewModel.PhoneNumber,
                };
                //Save in Database
                var result=await _userManager.CreateAsync(appUser, registerUserViewModel.Password);
                if (result.Succeeded)
                { // if succeeded Create Cookie
                    await _signInManager.SignInAsync(appUser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                //else
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
               

            }
            return View(registerUserViewModel);
        }
        #endregion
        #region Login
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel loginUserViewModel)
        {
            if (ModelState.IsValid)
            {
                //Check if the user found (by name)
                 var appUser=await _userManager.FindByNameAsync(loginUserViewModel.UserName);
                if (appUser != null)
                {
                    // if the user found Check on the password 
                    bool isEqual= await _userManager.CheckPasswordAsync(appUser, loginUserViewModel.Password);
                    if (isEqual)
                    {
                        //if the two Passwords are equal Create a Cookie for this user
                        await _signInManager.SignInAsync(appUser,loginUserViewModel.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "User Name or Password is In correct");
            }
            return View(loginUserViewModel);
        }
        #endregion
        #region Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Landing", "Home");
        }
        #endregion
        #region Forget Password

        #endregion
        #region Reset Password

        #endregion
    }
}
