using AltenBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltenBooking.Domain.Interfaces
{
    public interface IBookingRepository
    {
        
        IEnumerable<Booking> GetAll();
        IEnumerable<Booking> GetListBookingsByUser(string userId);
        bool VerifyAvailableDate(string dateIn, string dateOut);
        List<string> GetAllBusyDates();
        Booking GetById(int id);
        Task Create(Booking booking);
        Task Delete(int bookingId);
        Task UpdateBooking(Booking booking);
        
    }
}
