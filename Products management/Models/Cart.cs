using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Products_management.Models
{
    internal class Cart
    {
        [Key]
        public int Cartid { get; set; }
	    public int Userid { get; set; }
        [ForeignKey("Userid")]
        public User User { get; set; } 
        public int TotalPrice { get; set; }

    }
}
