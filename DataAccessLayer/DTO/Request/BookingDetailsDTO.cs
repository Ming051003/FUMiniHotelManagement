using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer.DTO;

namespace DataAccessLayer.DTO.Request
{
    public class BookingDetailsDTO
    {
        public RoomDTO Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? ActualPrice { get; set; }

        // Tính giá thực tế dựa trên giá phòng mỗi ngày và thời gian lưu trú
        public void CalculateActualPrice()
        {
            int days = (EndDate - StartDate).Days;
            ActualPrice = days * Room.RoomPricePerDay;
        }
    }
}
