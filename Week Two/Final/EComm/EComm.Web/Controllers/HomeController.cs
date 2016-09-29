using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EComm.DataAccess;
using EComm.Web.ViewModels;

namespace EComm.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IRepository repository)
            : base(repository) { }

        public IActionResult Index()
        {
            var model = new ProductListViewModel();
            model.Products = _repository.GetAllProducts().ToList();

            return View(model); 
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
