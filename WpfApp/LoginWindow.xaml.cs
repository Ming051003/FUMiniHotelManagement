using BusinessObjects.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interface;
using System.IO;
using System.Windows;
using WpfApp;

namespace WPFApp
{
    public partial class LoginWindow : Window
    {
        private readonly ICustomerService _service;

        public LoginWindow()
        {
            InitializeComponent();
            _service = ((App)Application.Current).ServiceProvider.GetRequiredService<ICustomerService>() ?? throw new ArgumentNullException(nameof(CustomerService));
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtEmail.Text) && !String.IsNullOrEmpty(txtPassword.Password))
            {
                if (IsAdmin(txtEmail.Text, txtPassword.Password))
                {
                    MessageBox.Show("Login Successful !! \nYou are Admin");

                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();

                    Close();
                }
                else
                {
                    Customer? customer = await _service.CheckLogin(txtEmail.Text, txtPassword.Password);

                    if (customer != null)
                    {
                        MessageBox.Show("Login Successful !!");

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.customer = customer;
                        mainWindow.Show();

                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong email or password!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid email or password!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private bool IsAdmin(string email, string password)
        {
            IConfiguration config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .Build();

            return email.Equals(config["AdminAccount:Email"])
                && password.Equals(config["AdminAccount:Password"]);
        }
    }
}
