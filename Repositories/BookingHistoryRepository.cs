using BusinessObjects.Models;
using DataAccessLayer;
using DataAccessLayer.DAO;
using DataAccessLayer.DTO;
using DataAccessObjects.DTO.Request;
using Repositories.Interface;

namespace Repositories
{
    public class BookingHistoryRepository : IBookingHistoryRepository
    {
        public async Task<BookingReservation?> GetBookingById(int id) => await BookingInfomationHistoryDAO.GetBookingById(id);

        public async Task<List<BookingInfomationHistoryDTO>> GetBookingByCusId(int id) => await BookingInfomationHistoryDAO.GetBookingByCustomerById(id);

        public BookingReservation CreateBooking(BookingDTO booking) => BookingInfomationHistoryDAO.CreateBooking(booking);

        public async Task UpdateBooking(BookingInfomationHistoryDTO booking) => await BookingInfomationHistoryDAO.UpdateBooking(booking);

        public async Task UpdateBooking(BookingReservation booking) => await BookingInfomationHistoryDAO.UpdateBooking(booking);

        public int CountBookings() => BookingInfomationHistoryDAO.CountBookings();

        public decimal? CalcSumRevenue() => BookingInfomationHistoryDAO.CalcSumRevenue();
    }
}
