using AltenBooking.Domain.Entities;
using AltenBooking.Service.Interface;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AltenBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingApiController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private IValidator<Booking> _validator;

        public BookingApiController(IBookingService bookingService, IValidator<Booking> validator)
        {
            _bookingService = bookingService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<Booking>> Get()
        {
            return Ok(_bookingService.GetAllBooking());
        }

        [HttpGet("BookingByUser/{userId}")]
        public async Task<ActionResult<Booking>> ListsBookingUser(string userId)
        {
            return Ok(_bookingService.GetAllListsBookingsUser(userId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetById(int id)
        {
            Booking bk = new Booking();
            bk = _bookingService.GetBookingById(id);
            return Ok(bk);
        }


        [HttpGet("{dateIn}/{dateOut}")]
        public async Task<ActionResult> GetAvailableDate(string dateIn, string dateOut)
        {
            var result = _bookingService.VerifyAvailableBookingDate(dateIn, dateOut);
            return Ok(result);
        }

        [HttpGet]
        [Route("BusyDates")]
        public async Task<ActionResult> GetAllBusyDates()
        {
            return Ok(_bookingService.GetAllBusyDates());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Booking booking)
        {
            ValidationResult result = await _validator.ValidateAsync(booking);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState, null);
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            await _bookingService.CreateBooking(booking);
            return Ok(booking);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Booking booking)
        {
            booking.ArriveDate = booking.ArriveDate.HasValue ? booking.ArriveDate : System.DateTime.MinValue;
            booking.OutDate = booking.OutDate.HasValue ? booking.OutDate : System.DateTime.MinValue;
            ValidationResult result = await _validator.ValidateAsync(booking);       
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState, null);
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            await _bookingService.UpdateBooking(booking);
            return Ok(booking);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _bookingService.DeleteBooking(id);
            
        }
    }
}
