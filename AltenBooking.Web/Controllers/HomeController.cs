using AltenBooking.Web.Models;
using AltenBooking.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AltenBooking.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookingService _bookingService;

        public HomeController(ILogger<HomeController> logger, IBookingService bookingService)
        {
            _logger = logger;
            _bookingService = bookingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAvabiality(string dateIn, string dateOut)
        {
            bool res = await _bookingService.GetAvailable(dateIn, dateOut);

            if (!res)
            {               
                TempData["Available"] = "Selected date is available for booking. Please register or login to succed!";
                return RedirectToAction("Index");
            }
            TempData["NotAvailable"] = "Selected date not available for booking!";
            return View("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
