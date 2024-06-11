using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAccessLayer.DTO;
using DataAccessObjects.DTO.Request;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DAO
{
    public class BookingInfomationHistoryDAO
    {
        public static async Task<BookingReservation?> GetBookingById(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return await db.BookingReservations.FirstOrDefaultAsync(br => br.Equals(id));
        }

        public static async Task<List<BookingInfomationHistoryDTO>> GetBookingByCustomerById(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return await db.BookingDetails
                .Include(bd => bd.BookingReservation)
                .ThenInclude(br => br.Customer)
                .Include(bd => bd.Room)
                .Where(bd => bd.BookingReservation.CustomerId == id 
                && bd.BookingReservation.BookingStatus == 1)
                .Select(bd => new BookingInfomationHistoryDTO
                {
                    BookingReservationId = bd.BookingReservationId,
                    RoomId = bd.RoomId,
                    RoomNumber = bd.Room.RoomNumber,
                    StartDate = bd.StartDate,
                    EndDate = bd.EndDate,
                    ActualPrice = bd.ActualPrice,
                    BookingDate = bd.BookingReservation.BookingDate,
                    TotalPrice = bd.BookingReservation.TotalPrice,
                    CustomerId = bd.BookingReservation.CustomerId,
                    BookingStatus = bd.BookingReservation.BookingStatus
                })
                .ToListAsync();

        }
        
        public static int CountBookings()
        {
            using var db = new FuminiHotelManagementContext ();
            return db.BookingReservations
                .Where(br => br.BookingStatus ==1)
                .Count();
        }

        public static decimal? CalcSumRevenue()
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingReservations
                .Where(br => br.BookingStatus ==1)
                .Sum(br => br.TotalPrice);
        }

        public static BookingReservation CreateBooking(BookingDTO bookingDTO)
        {
            using var db = new FuminiHotelManagementContext();

            var bookingReservation = new BookingReservation
            {
                BookingReservationId = db.BookingReservations.Count() + 1,
                BookingDate = DateOnly.FromDateTime((DateTime)bookingDTO.BookingDate),
                TotalPrice = bookingDTO.TotalPrice,
                CustomerId = bookingDTO.CustomerId,
                BookingStatus = bookingDTO.BookingStatus,
                BookingDetails = new List<BookingDetail>()
            };
            var booking = db.BookingReservations.Add(bookingReservation);
            db.SaveChanges();
            foreach (var detailDto in bookingDTO.BookingDetails)
            {
                var room = db.RoomInformations.Find(detailDto.Room.RoomId);

                room.RoomStatus = 0;
                db.SaveChanges();

                var detail = new BookingDetail
                {
                    BookingReservationId = booking.Entity.BookingReservationId,
                    RoomId = detailDto.Room.RoomId,
                    StartDate = DateOnly.FromDateTime(detailDto.StartDate),
                    EndDate = DateOnly.FromDateTime(detailDto.EndDate),
                    ActualPrice = detailDto.ActualPrice,
                    Room = room,
                };

                //db.BookingDetails.Add(detail);
                booking.Entity.BookingDetails.Add(detail);
            }

            db.BookingReservations.Update(booking.Entity);

            db.SaveChanges();

            return bookingReservation;
        }
        public static BookingDTO? GetBookingDTOById(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingReservations
                .Where(b => b.BookingReservationId == id)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.Room)
                .Select(b => new BookingDTO
                {
                }).FirstOrDefault();
        }

        public static async Task UpdateBooking(BookingInfomationHistoryDTO bookingDTO)
        {
            using var db = new FuminiHotelManagementContext();
            var bookingReservation = db.BookingReservations
                .Where(b => bookingDTO.BookingReservationId == b.BookingReservationId).FirstOrDefault();
            bookingReservation.BookingStatus = 0;
            db.BookingReservations.Update(bookingReservation);
            await db.SaveChangesAsync();
        }

        public static async Task UpdateBooking(BookingReservation booking)
        {
            using var db = new FuminiHotelManagementContext();
            booking.BookingStatus = 0;
            db.BookingReservations.Update(booking);
            await db.SaveChangesAsync();
        }
    }
    
}
