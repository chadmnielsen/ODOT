using EComm.DataAccess;
using EComm.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EComm.Model;

namespace EComm.Tests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void Homepage()
        {
            // Arrange

            var controller = new HomeController(new FakeRepository());

            // Act
            var r = controller.Index();

            // Assert
            Assert.IsInstanceOfType(r, typeof(ViewResult));
            // ...
        }
    }

    class FakeRepository : IRepository
    {
        public IEnumerable<Product> GetAllProducts()
        {
            return new List<Product>() {
                new Product { Id=1, ProductName="Product1", SupplierId=0, UnitPrice=1.00M, Package="Box", IsDiscontinued=false },
                new Product { Id=2, ProductName="Product2", SupplierId=0, UnitPrice=2.00M, Package="Box", IsDiscontinued=false }
            };
        }
    }

}
