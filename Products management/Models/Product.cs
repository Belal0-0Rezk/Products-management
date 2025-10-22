using Products_management.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Products_management.Models
{
    internal class Product
    {
        [Key]
        public int Productid { get; set; }
	    public string Decription {  get; set; }
	    public string Name { get; set; }
	    public int Price { get; set; }
        public int InStockQuantity { get; set; }


    }
}
