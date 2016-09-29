using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EComm.Model;

namespace EComm.DataAccess
{
    public class InMemoryRepository : IRepository
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
