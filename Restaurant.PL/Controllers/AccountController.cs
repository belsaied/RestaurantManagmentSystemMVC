using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.SendEmailService;
using Restaurant.DAL.Models;
using Restaurant.PL.ViewModels.Auth_ViewModels;
using System.Security.Claims;

namespace Restaurant.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISendEmailService _emailSender;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ISendEmailService emailSender,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

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
                var appUser = new ApplicationUser()
                {
                    UserName = registerUserViewModel.UserName,
                    Email = registerUserViewModel.Email,
                    Address = registerUserViewModel.Address,
                    PhoneNumber = registerUserViewModel.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(appUser, registerUserViewModel.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(appUser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

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
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel loginUserViewModel, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByNameAsync(loginUserViewModel.UserName);
                if (appUser != null)
                {
                    bool isEqual = await _userManager.CheckPasswordAsync(appUser, loginUserViewModel.Password);
                    if (isEqual)
                    {
                        await _signInManager.SignInAsync(appUser, loginUserViewModel.RememberMe);

                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "User Name or Password is incorrect");
            }
            return View(loginUserViewModel);
        }
        #endregion

        #region External Login (Google & Microsoft)
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string? returnUrl = null)
        {
            // Request a redirect to the external login provider
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
        {
            // FIX: Default to authenticated dashboard instead of root
            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return RedirectToAction(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await _signInManager.ExternalLoginSignInAsync(
                info.LoginProvider,
                info.ProviderKey,
                isPersistent: false,
                bypassTwoFactor: true);

            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.",
                    info.Principal.Identity?.Name, info.LoginProvider);

                // FIX: Validate returnUrl is local before redirecting
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                // FIX: Explicit redirect to authenticated dashboard
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, create one
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["Provider"] = info.LoginProvider;

                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var name = info.Principal.FindFirstValue(ClaimTypes.Name);

                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel
                {
                    Email = email ?? "",
                    Name = name ?? ""
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginConfirmation(
            ExternalLoginConfirmationViewModel model,
            string? returnUrl = null)
        {
            // FIX: Default to authenticated dashboard instead of root
            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    ModelState.AddModelError(string.Empty, "Error loading external login information.");
                    return View(nameof(ExternalLoginConfirmation));
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Address = model.Address ?? "Not provided",
                    EmailConfirmed = true // Auto-confirm email for OAuth users
                };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);

                        // FIX: Validate returnUrl is local before redirecting
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        // FIX: Explicit redirect to authenticated dashboard
                        return RedirectToAction("Index", "Home");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLoginConfirmation), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
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
                    var resetLink = Url.Action("ResetPassword", "Account",
                        new { email = user.Email, token = token }, Request.Scheme);

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

                    Email email = new Email()
                    {
                        To = user.Email!,
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
        public IActionResult ResetPassword(string email, string token)
        {
            if (email is null || token is null)
                return BadRequest();
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string email = TempData.Peek("email")?.ToString() ?? "";
                string token = TempData.Peek("token")?.ToString() ?? "";
                var user = _userManager.FindByEmailAsync(email).Result;
                if (user is not null)
                {
                    var res = _userManager.ResetPasswordAsync(user, token, model.Password).Result;
                    if (res.Succeeded)
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