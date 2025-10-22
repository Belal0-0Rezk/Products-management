using Products_management.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products_management.Models
{
    internal class CartItem
    {

        public int CartItemid { get; set; }
	    public int Cartid { get; set; }
        [ForeignKey("Cartid")]
        public Cart cart { get; set; }
        public int Productid { get; set; }
        [ForeignKey("Cartid")]
        public Product product { get; set; }
        public int Quantity { get; set; }


    }
}
