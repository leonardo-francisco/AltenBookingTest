using AltenBooking.Web.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AltenBooking.Web.Services
{
    public interface IBookingService
    {
        Task<List<BookingDTO>> GetAll();
        Task<List<BookingDTO>> ListsBkngUser(string userId);
        Task<BookingDTO> GetById(int id);
        Task<bool> GetAvailable(string dateIn, string dateOut);
        Task<List<string>> BusyDates();
        Task Create(BookingDTO dto);
        Task Update(BookingDTO dto);
        Task Delete(int id);


    }
}
