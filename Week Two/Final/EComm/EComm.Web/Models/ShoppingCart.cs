using EComm.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Web.Models
{
    public class ShoppingCart
    {
        public class LineItem
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
            public decimal TotalCost
            {
                get { return Product.UnitPrice.Value * Quantity; }
            }
        }

        public ShoppingCart()
        {
            LineItems = new List<LineItem>();
        }

        public List<LineItem> LineItems { get; set; }

        public decimal GrandTotal
        {
            get { return LineItems.Sum(item => item.TotalCost); }
        }

        public string FormattedGrandTotal
        {
            get { return String.Format("{0:C}", GrandTotal); }
        }

        public static ShoppingCart FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ShoppingCart>(json);
        }

        public string AsJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
