using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EComm.Web.ViewModels;
using System.Security.Claims;

namespace EComm.Web.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (!ModelState.IsValid) {
                return View(lvm);
            }
            bool auth = (lvm.Username == "test" && lvm.Password == "password");
            if (!auth) {
                return View(lvm);
            }
            var principal = new ClaimsPrincipal(
                new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, lvm.Username),
                    new Claim("IsAdmin", "true")
                }, "MyAuthSystem"));
            await HttpContext.Authentication.SignInAsync("Cookie", principal);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookie");
            return RedirectToAction("Login");
        }
    }
}
