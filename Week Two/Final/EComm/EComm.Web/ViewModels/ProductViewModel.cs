using EComm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Web.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel(Product product)
        {
            Id = product.Id;
            ProductName = product.ProductName;
            SupplierId = product.SupplierId;
            UnitPrice = product.UnitPrice;
            Package = product.Package;
            IsDiscontinued = product.IsDiscontinued;
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }
    }
}
