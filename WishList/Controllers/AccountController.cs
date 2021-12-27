using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View("Register", registerViewModel);
            }
            string password = "test";
            var user = new ApplicationUser { Email = registerViewModel.Email, UserName = registerViewModel.Email };
            var result = _userManager.CreateAsync(user);
            if(!result.Result.Succeeded)
            {
                foreach(var error in result.Result.Errors)
                {
                    ModelState.AddModelError(password, error.Description);
                }
                return View(registerViewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        
    }
}
