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
    /// Interaction logic for RoomManagement.xaml
    /// </summary>
    public partial class RoomManagement : UserControl
    {
        private readonly IRoomService iRoomService;
        public RoomManagement()
        {
            InitializeComponent();
            iRoomService = ((App)Application.Current).ServiceProvider.GetRequiredService<IRoomService>() ?? throw new ArgumentNullException(nameof(RoomService));
            LoadData();
        }

        private void LoadData()
        {
            dgRooms.ItemsSource = null;
            var rooms = iRoomService.GetRooms(r => r.RoomNumber.Contains(txtSearch.Text));
            dgRooms.ItemsSource = rooms;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSearch.Text))
            {
                try
                {
                    List<RoomDTO> rooms = iRoomService.GetRooms(r => r.RoomNumber.Contains(txtSearch.Text));
                    // Ensure UI update happens on the main thread
                    Dispatcher.Invoke(() =>
                    {
                        dgRooms.ItemsSource = null;
                        dgRooms.ItemsSource = rooms;
                    });
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                LoadData();
            }
        }

        private void dgRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addEditRoomDialog = new AddEditRoomDialog();
            if (addEditRoomDialog.ShowDialog() == true)
            {
                var newRoom = addEditRoomDialog.Room;
                await iRoomService.AddRoom(newRoom);
                LoadData();
            }
        }

        private async void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is RoomDTO selectedRoom)
            {
                var addEditRoomDialog = new AddEditRoomDialog(selectedRoom);
                if (addEditRoomDialog.ShowDialog() == true)
                {
                    var updatedRoom = addEditRoomDialog.Room;
                    await iRoomService.UpdateRoom(updatedRoom);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a room to edit.", "Edit Room", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is RoomDTO selectedRoom)
            {
                if (MessageBox.Show($"Are you sure you want to delete Room {selectedRoom.RoomNumber}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    /*Xóa khỏi db
                    await _service.DeleteRoom(selectedRoom.RoomId);
                    LoadData();
                    */

                    //Xóa chỉ chuyển trạng thái status
                    selectedRoom.RoomStatus = 0;
                    await iRoomService.UpdateRoom(selectedRoom);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a room to delete.", "Delete Room", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
