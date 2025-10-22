using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        ProductsDB db = new ProductsDB();
        public Admin()
        {
            InitializeComponent();
            LoadobjectinDatagrid();
        }

        public void LoadobjectinDatagrid()
        {
            var LoadobjectinDatagrid = db.Product.ToList();
            ProductsDataGrid.ItemsSource = LoadobjectinDatagrid;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = ProductNameTextBox.Text;
            string description = ProductDescTextBox.Text;
            if (!int.TryParse(ProductPriceTextBox.Text, out int price) || !int.TryParse(ProductQuantityTextBox.Text, out int inStockQuantity))
            {
                MessageBox.Show("Please enter valid numeric values for Price and In-Stock Quantity.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                Product pp = new Product()
                {
                    Decription = description,
                    Name = name,
                    Price = price,
                    InStockQuantity = inStockQuantity,
                };
                db.Product.Add(pp);
                db.SaveChanges();

                LoadobjectinDatagrid();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

            if (!int.TryParse(ProductidTextBox.Text, out int ProductID))
            {
                MessageBox.Show("Invalied ID");
                return;
            }

                var pe = db.Product.FirstOrDefault(a => ProductID == a.Productid);

            if (pe != null) { 
                pe.Productid = ProductID;
                pe.Decription = ProductDescTextBox.Text;
                pe.Name = ProductNameTextBox.Text;
                pe.Price = int.Parse(ProductPriceTextBox.Text);
                pe.InStockQuantity = int.Parse(ProductQuantityTextBox.Text);

                db.SaveChanges();
                LoadobjectinDatagrid();
            }
            else
            {
                MessageBox.Show("Not Found");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            if (!int.TryParse(ProductidTextBox.Text, out int ProductID))
            {
                MessageBox.Show("Please enter valid numeric values Product Id.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var pd = db.Product.FirstOrDefault(a => a.Productid == ProductID);
            if (pd != null)
            {
                db.Remove(pd);
                db.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Product', RESEED, 0);");
                db.SaveChanges();
                LoadobjectinDatagrid();
            }
            else
            {
                MessageBox.Show("Not Found");
            }

        }
    }
}
