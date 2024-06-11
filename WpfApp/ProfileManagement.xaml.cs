using BusinessObjects.Models;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interface;
using System.Windows;
using System.Windows.Controls;
using WpfApp;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for ProfileManagement.xaml
    /// </summary>
    public partial class ProfileManagement : UserControl
    {
        private readonly ICustomerService _customerService;
        public Customer Customer;
        public ProfileManagement()
        {
            InitializeComponent();
            _customerService = ((App)Application.Current).ServiceProvider.GetRequiredService<ICustomerService>() ?? throw new ArgumentNullException(nameof(CustomerService));
            Loaded += LoadData;
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            txtFullName.Text = Customer.CustomerFullName;
            txtEmail.Text = Customer.EmailAddress;
            txtTelephone.Text = Customer.Telephone;
            pwdPassword.Password = Customer.Password;
            dpBirthday.SelectedDate = Customer.CustomerBirthday;
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Customer.CustomerFullName = txtFullName.Text;
            Customer.Telephone = txtTelephone.Text;
            Customer.CustomerBirthday = dpBirthday.SelectedDate;
            Customer.Password = pwdPassword.Password;

            bool success = await _customerService.UpdateProfile(Customer);

            if (success)
            {
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Failed to update profile.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            txtFullName.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            pwdPassword.Password = string.Empty;
            dpBirthday.SelectedDate = null;
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            pwdPassword.Visibility = Visibility.Collapsed;
            txtPassword.Visibility = Visibility.Visible;
            txtPassword.Text = pwdPassword.Password;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            pwdPassword.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Collapsed;
            pwdPassword.Password = txtPassword.Text;
        }
    }
}
