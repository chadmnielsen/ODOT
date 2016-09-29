using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EComm.Web.ViewModels;
using System.Security.Claims;
using EComm.DataAccess;

namespace EComm.Web.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : BaseController
    {
        public AdminController(IRepository repository)
            : base(repository) { }

        public IActionResult Index()
        {
            var products = (from p in _repository.GetAllProducts()
                            orderby p.UnitPrice descending
                            select p).ToList();

            return View(products);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel lvm)
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
                }, "MyCoolAuthSystem"));
            HttpContext.Authentication.SignInAsync("Cookie", principal);
            return RedirectToAction("Index");
        }
    }
}
