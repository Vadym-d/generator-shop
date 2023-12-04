using Core.Entities;
using GeneratorShop.Models;
using Microsoft.AspNetCore.Authorization;
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
                        ModelState.AddModelError("Password", "Не вдалось увійти. Періврте свій пароль і пошту");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Не знайдено такого користувача");
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

                ModelState.AddModelError("", "Ой щось не ладне коїться");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Redirect("/");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UserManagment()
        {
            DbUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Phone = user.PhoneNumber,
                Email = user.Email,
            };

            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeProfile(EditUserViewModel model)
        {
            ModelState.Remove("NewPassword");
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                user.Email = model.Email;
                user.PhoneNumber = model.Phone;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return View("UserManagment", model); ; 
                }
                else
                {
                    ModelState.AddModelError("Phone", "Не вдалось оновити профіль. Спробуйте ще раз");
                }
            }

            return View("UserManagment", model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(EditUserViewModel model)
        {
            ModelState.Remove("Email");
            ModelState.Remove("Phone");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);

                if (passwordCheck)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

                    if (changePasswordResult.Succeeded)
                    {
                        return RedirectToAction("/");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Не вдалось змінити пароль. Спробуйте ще раз");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Не вірний старий пароль");
                }
            }

            return View("UserManagment", model);
        }
    }
}
