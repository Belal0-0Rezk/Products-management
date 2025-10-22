using Microsoft.EntityFrameworkCore;
using Products_management.Data;
using Products_management.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Products_management
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        ProductsDB db = new ProductsDB();

        User currentUser;

        public Customer(User userFromLogin)
        {
            InitializeComponent();
            Loadproduct();
            currentUser = userFromLogin;
        }
        public void Loadproduct()
        {
            var Loadproduct = db.Product.ToList();
            ProductsDataGridcart.ItemsSource = Loadproduct;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var selectedproduct = ProductsDataGridcart.SelectedItem as Product;
            if (selectedproduct == null)
            {
                errorselect.Text = "Please Select Product to Add to Cart";
                return;
            }

            if (!int.TryParse(Amount.Text, out int quantity) || quantity <= 0)
            {
                errorselect.Text = "Please Enter Valid Quantity";
                return;
            }
            else if (quantity > selectedproduct.InStockQuantity)
            {
                errorselect.Text = "Sorry, the available quantity is less than required";
                return;
            }
;
            var usercart = db.Cart.FirstOrDefault(c => c.Userid == currentUser.Userid);
            if (usercart == null)
            {
                usercart = new Cart
                {
                    Userid = currentUser.Userid,
                    TotalPrice = 0
                };
                db.Cart.Add(usercart);
                db.SaveChanges();
            }


            CartItem cartitem = new CartItem
            {
                Cartid = usercart.Cartid,
                Productid = selectedproduct.Productid,
                Quantity = quantity
            };
            db.CartItem.Add(cartitem);
            db.SaveChanges();

            usercart.TotalPrice += selectedproduct.Price * quantity;
            selectedproduct.InStockQuantity -= quantity;
            db.SaveChanges();
            MessageBox.Show("Added to cart successfully!");
        }

        private void ViewtoCart_Click_1(object sender, RoutedEventArgs e)
        {
            Cart1 cartwindow = new Cart1(currentUser);
            cartwindow.Show();
            this.Close();
        }
    }
}
