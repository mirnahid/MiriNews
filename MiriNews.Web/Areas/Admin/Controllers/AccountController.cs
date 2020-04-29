using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiriNews.Core.Entity.IdentityCore;
using MiriNews.Web.Areas.Admin.Models;

namespace MiriNews.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string returnUrl, LogInViewModel model)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            //var user = await _userManager.FindByNameAsync(model.UserName);

            //await _signInManager.SignOutAsync();
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
            if (result.Succeeded)
            {
                var user =await _userManager.FindByNameAsync(model.UserName);
                if (await _userManager.IsInRoleAsync(user,"admin"))
                {
                    if (string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);

                    }
                    return RedirectToAction("Index", "Home", new { area = "admin" });
                }
                if (string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);

                }
                return RedirectToAction("Index", "editors", new { area = "admin" });

            }

            ModelState.AddModelError("", "email və ya parol səhvdir");
            return View(model);


        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}