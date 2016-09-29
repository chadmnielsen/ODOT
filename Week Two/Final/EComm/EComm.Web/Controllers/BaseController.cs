using Microsoft.AspNetCore.Mvc;
using EComm.Web.Models;
using System.Text;
using EComm.DataAccess;

namespace EComm.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IRepository _repository;

        public BaseController(IRepository repository)
        {
            _repository = repository;
        }

        public ShoppingCart GetShoppingCart()
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
            return cart;
        }
    }
}
