using AltenBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltenBooking.Service.Interface
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetAllBooking();
        IEnumerable<Booking> GetAllListsBookingsUser(string userId);
        bool VerifyAvailableBookingDate(string dateIn, string dateOut);
        List<string> GetAllBusyDates();
        Booking GetBookingById(int id);
        Task CreateBooking(Booking booking);
        Task DeleteBooking(int bookingId);
        Task UpdateBooking(Booking booking);

    }
}
