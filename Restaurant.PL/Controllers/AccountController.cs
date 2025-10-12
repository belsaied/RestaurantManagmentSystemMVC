using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Restaurant.BLL.SendEmailService;
using Restaurant.DAL.Models;
using Restaurant.PL.ViewModels.Auth_ViewModels;

namespace Restaurant.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager
        , SignInManager<ApplicationUser> _signInManager,ISendEmailService _emailSender) : Controller
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
                    UserName = registerUserViewModel.UserName,
                    Email = registerUserViewModel.Email,
                    Address = registerUserViewModel.Address,
                    PhoneNumber = registerUserViewModel.PhoneNumber,
                };
                //Save in Database
                var result = await _userManager.CreateAsync(appUser, registerUserViewModel.Password);
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
                var appUser = await _userManager.FindByNameAsync(loginUserViewModel.UserName);
                if (appUser != null)
                {
                    // if the user found Check on the password 
                    bool isEqual = await _userManager.CheckPasswordAsync(appUser, loginUserViewModel.Password);
                    if (isEqual)
                    {
                        //if the two Passwords are equal Create a Cookie for this user
                        await _signInManager.SignInAsync(appUser, loginUserViewModel.RememberMe);
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
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendResetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(model.Email).Result;
                if (user is not null)
                {

                    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var resetLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token = token }, Request.Scheme);
                    //send email
                    #region Body
                    string body = $@"
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Reset Your Password</title>
    <style>
        body {{
            font-family: 'Segoe UI', Arial, sans-serif;
            background-color: #fffaf5;
            color: #333;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 480px;
            margin: 40px auto;
            background: #fff;
            border-radius: 12px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.07);
            padding: 32px 24px;
        }}
        .header {{
            text-align: center;
            color: #ff7f27;
            font-size: 2rem;
            font-weight: bold;
            margin-bottom: 12px;
        }}
        .icon {{
            font-size: 48px;
            color: #ff7f27;
            text-align: center;
            margin-bottom: 16px;
        }}
        .btn {{
            display: inline-block;
            padding: 12px 32px;
            font-size: 1rem;
            color: #fff !important;
            background-color: #ff7f27;
            border-radius: 8px;
            text-decoration: none;
            font-weight: 600;
            margin: 24px 0;
            transition: background 0.2s;
        }}
        .btn:hover {{
            background-color: #ff944d;
        }}
        .footer {{
            font-size: 0.95rem;
            color: #888;
            text-align: center;
            margin-top: 32px;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='icon'>🔒</div>
        <div class='header'>Reset Your Password</div>
        <p>Hello <b>{user.UserName}</b>,</p>
        <p>We received a request to reset the password for your Krusty Krab Restaurant account.</p>
        <p>To reset your password, please click the button below:</p>
        <div style='text-align:center;'>
            <a href='{resetLink}' class='btn'>Reset Password</a>
        </div>
        <p>If you did not request a password reset, you can safely ignore this email.</p>
        <div class='footer'>
            &copy; {DateTime.Now.Year} Krusty Krab Restaurant. All rights reserved.
        </div>
    </div>
</body>
</html>";
                    #endregion
                    Email email = new Email()
                    {
                        To = user.Email,
                        Subject = "Reset Password",
                        Body = body
                    };
                    _emailSender.SendEmail(email);

                    return RedirectToAction("CheckInbox");

                }
                else
                {
                    ModelState.AddModelError("", "Invalid Operation");
                }
            }
            ModelState.AddModelError("", "Invalid Operation");

            return View("ForgetPassword", model);
        }

        public IActionResult CheckInbox()
        {
            return View();
        }
        #endregion
        #region Reset Password
        [HttpGet]
        public IActionResult ResetPassword(string email, string token){
            
            if(email is null || token is null)
                return BadRequest();
            TempData["email"] = email;
            TempData["token"] = token;
            return View();



        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model) {

            if (ModelState.IsValid)
            {
                string email = TempData.Peek("email")?.ToString() ?? "";
                string token = TempData.Peek("token")?.ToString() ?? "";
                var user = _userManager.FindByEmailAsync(email).Result;
                if(user is not null)
                {
                    var res=_userManager.ResetPasswordAsync(user,token,model.Password).Result;
                    if(res.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var error in res.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                

            }
            
                ModelState.AddModelError("", "Invalid Operation");
            
            return View(model);
        }
        #endregion
    }
}

