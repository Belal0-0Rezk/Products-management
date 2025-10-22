using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products_management
{
    public class CartItemViewModel
    {
        public string ProductName { get; set; }
        public int Pricee { get; set; }
        public int Quanttity { get; set; }
        public int Tottal => Pricee * Quanttity;
    }

}
