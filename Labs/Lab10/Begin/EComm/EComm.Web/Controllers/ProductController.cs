﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EComm.DataAccess;

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

            return PartialView();
        }
    }
}
