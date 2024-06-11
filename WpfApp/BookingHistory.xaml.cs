using BusinessObjects.Models;
using DataAccessLayer.DTO;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interface;
using System.Windows;
using System.Windows.Controls;
using WpfApp;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for BookingHistory.xaml
    /// </summary>
    public partial class BookingHistory : UserControl
    {
        private readonly IBookingHistoryService _service;
        public Customer Customer;
        public BookingHistory()
        {
            InitializeComponent();
            _service = ((App)Application.Current).ServiceProvider.GetRequiredService<IBookingHistoryService>() ?? throw new ArgumentNullException(nameof(BookingHistoryService));
            Loaded += LoadData;
        }

        private async void LoadData(object sender, RoutedEventArgs e)
        {
            List<BookingInfomationHistoryDTO> bookingDetails = await _service.GetBookingByCusId(Customer.CustomerId);

            dgBookingHistory.ItemsSource = bookingDetails;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditBookingDialog addEditBookingDialog = new AddEditBookingDialog();
            addEditBookingDialog.Customer = Customer;
            if (addEditBookingDialog.ShowDialog() == true)
            {
                LoadData(sender, e);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditBookingDialog addEditBookingDialog = new AddEditBookingDialog();
            addEditBookingDialog.Customer = Customer;
            //addEditBookingDialog.Booking = 
            if (addEditBookingDialog.ShowDialog() == true)
            {
                LoadData(sender, e);
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgBookingHistory.SelectedItem is BookingInfomationHistoryDTO selectedBooking)
            {
                if (MessageBox.Show($"Are you sure you want to delete Customer {selectedBooking.BookingReservationId}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    await _service.UpdateBooking(selectedBooking);
                    LoadData(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to delete.", "Delete Booking", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
    