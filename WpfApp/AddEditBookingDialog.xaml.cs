using BusinessObjects;
using BusinessObjects.Models;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Request;
using DataAccessObjects.DTO.Request;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interface;
using System.Windows;
using System.Windows.Controls;
using WpfApp;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AddEditBookingDialog.xaml
    /// </summary>
    public partial class AddEditBookingDialog : Window
    {
        private readonly IBookingHistoryService _bookingService;
        private readonly IRoomService _roomService;
        public BookingDTO Booking { get; private set; }
        public Customer Customer { get; set; }
        private RoomDTO room { get; set; }
        private List<BookingDetailsDTO> bookingDetailsDTOs { get; set; }

        public AddEditBookingDialog()
        {
            InitializeComponent();
            _bookingService = ((App)Application.Current).ServiceProvider.GetRequiredService<IBookingHistoryService>() ?? throw new ArgumentNullException(nameof(BookingHistoryService));
            _roomService = ((App)Application.Current).ServiceProvider.GetRequiredService<IRoomService>() ?? throw new ArgumentNullException(nameof(RoomService));
            if (Booking is null)
            {
                Booking = new BookingDTO();
                bookingDetailsDTOs = new List<BookingDetailsDTO>();
            }
            else
            {
                bookingDetailsDTOs = Booking.BookingDetails;
            }
            Loaded += LoadRooms;
            Loaded += LoadBookingDetails;
        }

        private void LoadRooms(object sender, RoutedEventArgs e)
        {
            var data = _roomService.GetRooms(r => r.RoomStatus == 1);
            dgAvailableRooms.ItemsSource = data;
        }

        private void LoadBookingDetails(object sender, RoutedEventArgs e)
        {
            dgBookingDetails.ItemsSource = null;
            dgBookingDetails.ItemsSource = bookingDetailsDTOs;

            txtTotalPrice.Text = CalculateTotalPrice().ToString();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dgAvailableRooms.SelectedItems.Count > 0)
            {
                room = (RoomDTO)dgAvailableRooms.SelectedItem;

                DateTime startDate = dpStartDate.SelectedDate.Value;
                DateTime endDate = dpEndDate.SelectedDate.Value;
                int daysDifference = (endDate - startDate).Days;
                var TotalPrice = (daysDifference * room.RoomPricePerDay);

                var bookingDetail = new BookingDetailsDTO()
                {
                    Room = room,
                    StartDate = dpStartDate.SelectedDate.Value,
                    EndDate = dpEndDate.SelectedDate.Value,
                    ActualPrice = TotalPrice,
                };
                bookingDetailsDTOs.Add(bookingDetail);
                LoadBookingDetails(sender, e);
            }
        }

        private decimal? CalculateTotalPrice()
        {
            decimal? total = 0;

            foreach (var item in bookingDetailsDTOs)
            {
                total += item.ActualPrice;
            }

            return total;
        }

        private void dgAvailableRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAvailableRooms.SelectedItems.Count > 0)
            {
                room = (RoomDTO)dgAvailableRooms.SelectedItem;
                txtRoomId.Text = room.RoomId.ToString();
                txtRoomNumber.Text = room.RoomNumber;
                txtNumPerson.Text = room.RoomMaxCapacity.ToString();
                txtPrice.Text = room.RoomPricePerDay.ToString();
            }
        }

        private void dpDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpStartDate.SelectedDate.HasValue && dpEndDate.SelectedDate.HasValue)
            {
                DateTime startDate = dpStartDate.SelectedDate.Value;
                DateTime endDate = dpEndDate.SelectedDate.Value;

                int daysDifference = (endDate - startDate).Days;
                var TotalPrice = (daysDifference * room.RoomPricePerDay);
                txtTotalPrice.Text = TotalPrice.ToString();

                if (startDate < DateTime.Today || endDate < DateTime.Today)
                {
                    txtTotalPrice.Text = "0";
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgBookingDetails.SelectedItems.Count > 0)
            {
                var bookingdetail = (BookingDetailsDTO)dgBookingDetails.SelectedItem;
                bookingDetailsDTOs.Remove(bookingdetail);
                LoadBookingDetails(sender, e);
            }
        }

        private void dgBookingDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Booking.BookingDate = DateTime.Today;
            Booking.TotalPrice = System.Convert.ToDecimal(txtTotalPrice.Text);
            Booking.CustomerId = Customer.CustomerId;
            Booking.BookingStatus = 1;
            Booking.BookingDetails = bookingDetailsDTOs;
            Booking.CalculateTotalPrice();

            _bookingService.CreateBooking(Booking);

            DialogResult = true;
            Close();
        }
    }
}
