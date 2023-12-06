using Core.Entities;
using GeneratorShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeneratorShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<DbUser> _signInManager;
        private readonly UserManager<DbUser> _userManager;
        public AccountController(SignInManager<DbUser> signInManager, UserManager<DbUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

                    if (result.Succeeded)
                    {
                        if (model.ReturnUrl == null)
                            return RedirectToAction("Index", "Home");

                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Invalid login attempt. Please check your email and password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "User not found. Please check your email and try again.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new DbUser()
                {
                    UserName = model.Email,
                    PhoneNumber = model.Phone,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, true);

                    return Redirect(model.ReturnUrl);
                }

                ModelState.AddModelError("", "Oooops some errors");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            return Redirect(returnUrl);
        }
    }
}
