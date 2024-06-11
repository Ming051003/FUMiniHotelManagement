using DataAccessLayer.DTO;
using System.Windows;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AddEditRoomDialog.xaml
    /// </summary>
    public partial class AddEditRoomDialog : Window
    {
        public RoomDTO Room { get; private set; }

        public AddEditRoomDialog(RoomDTO room = null)
        {
            InitializeComponent();
            if (room != null)
            {
                Room = room;
                txtRoomNumber.Text = room.RoomNumber;
                txtDescription.Text = room.RoomDetailDescription;
                txtMaxCapacity.Text = room.RoomMaxCapacity.ToString();;
                txtPrice.Text = room.RoomPricePerDay.ToString();
                txtType.Text = room.RoomType;
            }
            else
            {
                Room = new RoomDTO();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRoomNumber.Text) &&
                !string.IsNullOrEmpty(txtDescription.Text) &&
                !string.IsNullOrEmpty(txtMaxCapacity.Text) &&
                !string.IsNullOrEmpty(txtPrice.Text) &&
                !string.IsNullOrEmpty(txtType.Text))
            {
                // All text fields are not null or empty, proceed with assigning values
                Room.RoomNumber = txtRoomNumber.Text;
                Room.RoomDetailDescription = txtDescription.Text;
                Room.RoomMaxCapacity = int.Parse(txtMaxCapacity.Text);
                Room.RoomStatus = 1;
                Room.RoomPricePerDay = decimal.Parse(txtPrice.Text);
                Room.RoomType = txtType.Text;

                DialogResult = true;
                Close();
            }
            else
            {
                // Show a message indicating that all fields are required
                //MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
