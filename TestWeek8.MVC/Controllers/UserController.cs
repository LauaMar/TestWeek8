using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestWeek8.Core.Interfaces;
using TestWeek8.Core.Models;
using TestWeek8.MVC.Models;

namespace TestWeek8.MVC.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IMainBusinessLayer mainBL;
        public UserController(IMainBusinessLayer bl)
        {
            mainBL = bl;
        }
        public IActionResult Login(string returnURL)
        {
            return View(new UserViewModel { ReturnUrl = returnURL });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel uvm)
        {
            if (uvm == null)
            {
                return View("Error", new ResultBL(false, "Invalid User"));
            }

            var user = mainBL.GetUserByEmail(uvm.Email);
            if (user != null && ModelState.IsValid)
            {

                if (user.Password.Equals(uvm.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    };

                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                        RedirectUri = uvm.ReturnUrl
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                    );
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError(nameof(uvm.Password), "Incorrect Password");
                    return View(uvm);
                }
            }
            else
            {
                ModelState.AddModelError(nameof(uvm.Email), "Invalid User Data");
                return View(uvm);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
