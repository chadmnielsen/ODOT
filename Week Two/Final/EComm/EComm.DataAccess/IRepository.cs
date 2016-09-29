using EComm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.DataAccess
{
    public interface IRepository
    {
        IEnumerable<Product> GetAllProducts();
        void SaveProduct(Product product);

        IEnumerable<Supplier> GetAllSuppliers();
    }
}
