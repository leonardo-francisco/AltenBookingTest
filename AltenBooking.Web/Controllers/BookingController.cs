using AltenBooking.Domain.Entities;
using AltenBooking.Web.Models.DTO;
using AltenBooking.Web.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AltenBooking.Web.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookingService _bookService;
        private IValidator<BookingDTO> _validator;

        public BookingController(UserManager<ApplicationUser> userManager, IBookingService bookService, IValidator<BookingDTO> validator)
        {
            _userManager = userManager;
            _bookService = bookService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var boo = await _bookService.ListsBkngUser(userId);
            
            return View(boo);
        }

        [HttpGet]
        public IActionResult CreateBooking()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBooking(BookingDTO bookingDTO)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            ValidationResult result = _validator.Validate(bookingDTO);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState, null);
                return View("CreateBooking");
            }
            var model = new BookingDTO()
            {
                Room = bookingDTO.Room,
                NumberGuests = bookingDTO.NumberGuests,
                ArriveDate = bookingDTO.ArriveDate,
                OutDate = bookingDTO.OutDate,
                Request = bookingDTO.Request,
                Created = System.DateTime.Now,
                UserId = userId
            };
            _bookService.Create(model);
            TempData["crtSucc"] = "Congratulations! Booking created successfuly!";
            return RedirectToAction("Index", "Booking", new { id = userId });
        }

        [HttpGet]
        public async Task<IActionResult> EditBooking(int id)
        {
            var model = await _bookService.GetById(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditBooking(BookingDTO bookingDTO)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            bookingDTO.ArriveDate = bookingDTO.ArriveDate.HasValue ? bookingDTO.ArriveDate: System.DateTime.MinValue;
            bookingDTO.OutDate = bookingDTO.OutDate.HasValue ? bookingDTO.OutDate : System.DateTime.MinValue;       
            ValidationResult result = _validator.Validate(bookingDTO);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState, null);
                return View("EditBooking");
            }
            var model = new BookingDTO()
            {
                Id = bookingDTO.Id,
                Room = bookingDTO.Room,
                NumberGuests = bookingDTO.NumberGuests,
                ArriveDate = bookingDTO.ArriveDate,
                OutDate = bookingDTO.OutDate,
                Request = bookingDTO.Request,             
            };
            _bookService.Update(model);
            TempData["edtSucc"] = "Booking was edited successfuly!";
            return RedirectToAction("Index", "Booking", new {id = userId });
        }

        [HttpGet]
        public async Task<List<string>> GetBusyDates()
        {
            var x = await _bookService.BusyDates();
            return x;
        }

     
       [HttpDelete]
       public void DeleteBooking(int id)
        {
            _bookService.Delete(id);
        }


        
    }
}
