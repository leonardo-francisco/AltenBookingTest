using AltenBooking.Domain.Entities;
using AltenBooking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltenBooking.Data.Repository
{
    public class BookingRepository :IBookingRepository
    {       
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {        
            _context = context;
        }

        public async Task Create(Booking booking)
        {
            _context.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int bookingId)
        {
            var booking = GetById(bookingId);
            _context.Remove(booking);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Booking> GetAll()
        {
            return _context.Bookings.Include(booking => booking.User);
        }

        public List<string> GetAllBusyDates()
        {
            string ss = null; string aa = null;          
            var myArrayIn = _context.Bookings.Select(c => c.ArriveDate);
            var myArrayOut = _context.Bookings.Select(t => t.OutDate);
            List<string> pp = new List<string>();

            foreach (var item in myArrayIn)
            {
                //ss = item.ToString("yyyy-MM-dd h:mm tt").Replace("AM","").Replace("PM","");
                ss = item.HasValue ? item.Value.ToString("yyyy-MM-dd h:mm tt").Replace("AM", "").Replace("PM", "") : string.Empty;
                pp.Add(ss);              
            }
            foreach (var item in myArrayOut)
            {
                //aa = item.ToString("yyyy-MM-dd h:mm tt").Replace("AM", "").Replace("PM", "");
                aa = item.HasValue ? item.Value.ToString("yyyy-MM-dd h:mm tt").Replace("AM", "").Replace("PM", "") : string.Empty;
                pp.Add(aa);
            }
            
            return pp;
        }

        public Booking GetById(int id)
        {
            var booking = _context.Bookings.Where(f => f.Id == id)
                .Include(b => b.User)
                .FirstOrDefault();

            return booking;
        }

        public IEnumerable<Booking> GetListBookingsByUser(string userId)
        {
            return _context.Bookings.Include(booking => booking.User).Where(r => r.UserId == userId);
        }

        public async Task UpdateBooking(Booking booking)
        {
            try
            {
                var bok = GetById(booking.Id);
                bok = new Booking()
                {
                    Id = booking.Id,
                    Room = booking.Room,
                    NumberGuests = booking.NumberGuests,
                    ArriveDate = booking.ArriveDate,
                    OutDate = booking.OutDate,
                    Request = booking.Request,
                    User = bok.User,
                    UserId = bok.UserId
                };
                _context.ChangeTracker.Clear();
                _context.Bookings.Update(bok);
                //_context.Entry(bok).State.IsModified = true;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool VerifyAvailableDate(string dateIn, string dateOut)
        {
            var bk = _context.Bookings.Where(b => b.ArriveDate >= Convert.ToDateTime(dateIn)
                                              && b.OutDate <= Convert.ToDateTime(dateOut));

            if (!bk.Any()) return false;

            return true;
        }
    }
}
