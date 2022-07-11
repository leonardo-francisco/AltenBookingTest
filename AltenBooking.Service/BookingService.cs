using AltenBooking.Domain.Entities;
using AltenBooking.Domain.Interfaces;
using AltenBooking.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltenBooking.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public Task CreateBooking(Booking booking)
        {
            return _bookingRepository.Create(booking);
        }

        public Task DeleteBooking(int bookingId)
        {
            return _bookingRepository.Delete(bookingId);
        }

        public IEnumerable<Booking> GetAllBooking()
        {
            return _bookingRepository.GetAll();
        }

        public List<string> GetAllBusyDates()
        {
            return _bookingRepository.GetAllBusyDates();
        }

        public IEnumerable<Booking> GetAllListsBookingsUser(string userId)
        {
            return _bookingRepository.GetListBookingsByUser(userId);
        }

        public Booking GetBookingById(int id)
        {
            return _bookingRepository.GetById(id);
        }


        public Task UpdateBooking(Booking booking)
        {
            return _bookingRepository.UpdateBooking(booking);
        }

        public bool VerifyAvailableBookingDate(string dateIn, string dateOut)
        {
            return _bookingRepository.VerifyAvailableDate(dateIn, dateOut);
        }
    }
}
