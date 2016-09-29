using EComm.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Web.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        { }

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

        [Required]
        [Display(Name ="Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        public IEnumerable<SelectListItem> Suppliers { get; set; }

        [Display(Name = "Unit Price")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Package")]
        public string Package { get; set; }

        [Display(Name = "Discontinued?")]
        public bool IsDiscontinued { get; set; }
    }
}
