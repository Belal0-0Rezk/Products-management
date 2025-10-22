using Products_management.Data;
using Products_management.Models;
using System.Windows;

namespace Products_management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProductsDB db = new ProductsDB();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string Email = EmailTextBox.Text;
            if (!int.TryParse(PasswordBox.Password , out int pass))
            {
                PassErrorDB.Text = "Please Enter Avalied Password!!";
                return;
            }

            var user = db.User.FirstOrDefault(u => u.useremail == Email && u.userpassword == pass);
            if (user == null)
            {
                ErrorDB.Text = "Your Account is Not Defined";
            }
            else
            {
                if (user.Role == "Customer")
                {
                    Customer customer = new Customer(user);
                    customer.Show();
                    this.Close();
                }else if (user.Role == "Admin")
                {
                    Admin admin = new Admin();
                    admin.Show();
                    this.Close();
                }
            }
        }
    }
}