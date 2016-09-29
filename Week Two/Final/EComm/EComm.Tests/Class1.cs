using EComm.DataAccess;
using EComm.Web.Controllers;
using EComm.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EComm.Tests
{
    public class Class1
    {
        [Fact]
        public void SimpleTest()
        {
            // Arrange
            int i = 5;

            // Act
            i += 1;

            // Assert
            Assert.Equal(6, i);
        }

        [Fact]
        public void GetListOfProducts()
        {
            // Arrange
            var hc = new HomeController(new InMemoryRepository());

            // Act
            var r = hc.Index();

            // Assert
            Assert.IsType(typeof(ViewResult), r);
            var view = (r as ViewResult);
            Assert.IsType(typeof(ProductListViewModel), view.Model);
            var m = (view.Model as ProductListViewModel);
            Assert.True(m.Products.Count() == 3);
        }

        [Fact]
        public void CheckoutWithValidData()
        {
            // Arrange
            var c = new ProductController(new InMemoryRepository());
            var scvm = new ShoppingCartViewModel()
            {
                CustomerName = "Joe Smith",
                Email = "a@b.com",
                CreditCard = "123"
            };

            // Act
            var r = c.Checkout(scvm);

            // Assert
            //
        }
    }
}
