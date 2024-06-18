using CarbonFootprintApp.Models;
using CarbonFootprintApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarbonFootprintApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager
                            .PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.IsRememberMe, false);

                if (result.Succeeded)
                {
                    var loginUserInfo = await _userManager.FindByEmailAsync(loginModel.Email);
                    TempData["LoginNotifaction"] = "Login Successfull";

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "FootprintHistory");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(loginModel);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    UserName = registerModel.Email,
                    Email = registerModel.Email,
                    City = registerModel.City
                };

                var createUserResult = await _userManager.CreateAsync(user, registerModel.Password);
                var setUserRole = await _userManager.AddToRoleAsync(user, "User");

                if (createUserResult.Succeeded && setUserRole.Succeeded)
                {
                    // await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["EmployeeRegistrationNotifaction"] = "Registration Successfull";
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in createUserResult.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(registerModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}