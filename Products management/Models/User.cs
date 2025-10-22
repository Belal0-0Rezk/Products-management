using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products_management.Models
{
    public class User
    {
        [Key]
        public int Userid { get; set; }
	    public string username { get; set; }
	    public string useremail { get; set; }
	    public int userpassword { get; set; }
        public string Role {get; set; } 
    }
}
