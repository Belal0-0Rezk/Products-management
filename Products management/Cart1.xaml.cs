using Products_management.Data;
using Products_management.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart1 : Window 
    {
        ProductsDB db = new ProductsDB();
        User currentUser;
        public Cart1(User user)
        {
            InitializeComponent();
            currentUser = user;
            Loadproduct();
        }
        public void Loadproduct()
        {
        var usercart = db.Cart.FirstOrDefault(c => c.Userid == currentUser.Userid);
            if (usercart == null)
            {
                MessageBox.Show("Your cart is empty.");
            }


        var cartitem = db.CartItem
                .Where(ci => ci.Cartid == usercart.Cartid)
                .Select(ci => new{
                    ProductName = ci.product.Name,
                    Pricee = ci.product.Price,
                    Quanttity = ci.Quantity,
                    //Tottal = ci.product.Price * ci.Quantity
                }).ToList();

            ProductsDataGridcart.ItemsSource = cartitem;
            
        }

        private void checkout_Click(object sender, RoutedEventArgs e)
        {
            var userCart = db.Cart.FirstOrDefault(c => c.Userid == currentUser.Userid);

            if (userCart == null || userCart.TotalPrice == 0)
            {
                MessageBox.Show("Your cart is empty!");
                return;
            }

            MessageBox.Show($"Purchase completed successfully!\nTotal = {userCart.TotalPrice} $");

            var cartItems = db.CartItem.Where(ci => ci.Cartid == userCart.Cartid).ToList();
            db.CartItem.RemoveRange(cartItems);
            userCart.TotalPrice = 0;

            db.SaveChanges();

            Loadproduct();
        }

        private void BacktoProducts_Click(object sender, RoutedEventArgs e)
        {
            Customer Customer = new Customer(currentUser);
            Customer.Show();
            this.Close();
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ProductsDataGridcart.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Please select an item to remove!");
                return;
            }

            var userCart = db.Cart.FirstOrDefault(c => c.Userid == currentUser.Userid);
            if (userCart == null)
            {
                MessageBox.Show("No cart found!");
                return;
            }

            string productName = (string)selectedItem.GetType().GetProperty("ProductName")?.GetValue(selectedItem);

            var product = db.Product.FirstOrDefault(p => p.Name == productName);
            var cartItem = db.CartItem.FirstOrDefault(ci => ci.Productid == product.Productid && ci.Cartid == userCart.Cartid);

            if (cartItem != null)
            {
                product.InStockQuantity += cartItem.Quantity;

                userCart.TotalPrice -= cartItem.Quantity * product.Price;

                db.CartItem.Remove(cartItem);
                db.SaveChanges();

                MessageBox.Show("Item removed from cart!");

                Loadproduct();
            }
        }
    }
    
}
