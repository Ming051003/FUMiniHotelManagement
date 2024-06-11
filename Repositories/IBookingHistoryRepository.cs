using BusinessObjects;
using BusinessObjects.Models;
using DataAccessLayer.DTO;
using DataAccessObjects.DTO.Request;

namespace Repositories.Interface
{
    public interface IBookingHistoryRepository
    {
        Task<BookingReservation?> GetBookingById(int id);
        Task<List<BookingInfomationHistoryDTO>> GetBookingByCusId(int id);
        BookingReservation CreateBooking(BookingDTO booking);
        Task UpdateBooking(BookingInfomationHistoryDTO booking);
        Task UpdateBooking(BookingReservation booking);
        int CountBookings();
        decimal? CalcSumRevenue();
    }
}
