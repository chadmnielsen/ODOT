using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EComm.DataAccess;
using EComm.Web.Models;
using System.Text;
using EComm.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace EComm.Web.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IRepository repository)
            : base(repository) { }

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

            ShoppingCart cart = GetShoppingCart();

            var lineItem = cart.LineItems.SingleOrDefault(item => item.Product.Id == id);
            if (lineItem != null) {
                lineItem.Quantity += quantity;
            }
            else {
                cart.LineItems.Add(new ShoppingCart.LineItem { Product = product, Quantity = quantity });
            }
            var data = Encoding.UTF8.GetBytes(cart.AsJson());
            HttpContext.Session.Set("ShoppingCart", data);

            return PartialView();
        }

        public IActionResult Cart()
        {
            ShoppingCart cart = GetShoppingCart();
            var model = new ShoppingCartViewModel();
            model.Cart = cart;
            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(ShoppingCartViewModel scvm)
        {
            if (!ModelState.IsValid) {
                scvm.Cart = GetShoppingCart();
                return View("Cart", scvm);
            }
            // TODO: Charge the customer's card and record the order
            HttpContext.Session.Clear();
            return View("ThankYou");
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Edit(int id)
        {
            var product = _repository.GetAllProducts().SingleOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            var pvm = new ProductViewModel(product);

            return View(pvm);
        }
    }
}
