using BusinessObjects.Models;
using DataAccessLayer.DTO;
using DataAccessObjects.DTO.Request;

namespace Services.Interface
{
    public interface IBookingHistoryService
    {
        Task<BookingReservation?> GetBookingById(int id);
        Task<List<BookingInfomationHistoryDTO>> GetBookingByCusId(int id);
        BookingReservation CreateBooking(BookingDTO booking);
        Task UpdateBooking(BookingInfomationHistoryDTO booking);
        int CountBookings();
        decimal? CalcRevenue();
    }
}
