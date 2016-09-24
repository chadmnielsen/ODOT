using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EComm.DataAccess;
using EComm.Web.Models;
using System.Text;
using EComm.Web.ViewModels;

namespace EComm.Web.Controllers
{
    public class ProductController : Controller
    {
        private IRepository _repository;

        public ProductController(IRepository repository)
        {
            _repository = repository;
        }

        [Route("product/{id:int}")]
        public IActionResult Detail(int id)
        {
            var model = _repository.GetAllProducts().SingleOrDefault(p => p.Id == id);

            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            var product = _repository.GetAllProducts().SingleOrDefault(p => p.Id == id);
            var totalCost = quantity * product.UnitPrice;

            string message = $"You added {product.ProductName} " +
                $"(x{quantity}) to your cart " +
                $"at a total cost of {totalCost:C}.";

            ViewBag.Message = message;

            byte[] data;
            ShoppingCart cart;
            bool b = HttpContext.Session.TryGetValue("ShoppingCart", out data);
            if (b) {
                cart = ShoppingCart.FromJson(Encoding.UTF8.GetString(data));
            } else {
                cart = new ShoppingCart();
            }
            var lineItem = cart.LineItems.SingleOrDefault(item => item.Product.Id == id);
            if (lineItem != null) {
                lineItem.Quantity += quantity;
            }
            else {
                cart.LineItems.Add(new ShoppingCart.LineItem { Product = product, Quantity = quantity });
            }
            data = Encoding.UTF8.GetBytes(cart.AsJson());
            HttpContext.Session.Set("ShoppingCart", data);

            return PartialView();
        }

        public IActionResult Cart()
        {
            byte[] data;
            ShoppingCart cart;
            bool b = HttpContext.Session.TryGetValue("ShoppingCart", out data);
            if (b) {
                cart = ShoppingCart.FromJson(Encoding.UTF8.GetString(data));
            }
            else {
                cart = new ShoppingCart();
            }
            var model = new ShoppingCartViewModel();
            model.Cart = cart;
            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(ShoppingCartViewModel scvm)
        {
            if (!ModelState.IsValid) {
                // TODO: Set scvm.Cart to the shopping cart
                return View(scvm);
            }
            // TODO: Charge the customer's card and record the order
            HttpContext.Session.Clear();
            return View("ThankYou");
        }
    }
}
