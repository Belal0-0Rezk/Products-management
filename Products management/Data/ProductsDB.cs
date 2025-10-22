using Microsoft.EntityFrameworkCore;
using Products_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products_management.Data
{
    internal class ProductsDB : DbContext
    {
        public ProductsDB(){}

        public ProductsDB(DbContextOptions options) : base(options){}

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-K5NGEGR;Initial Catalog=Products_managementDB;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
