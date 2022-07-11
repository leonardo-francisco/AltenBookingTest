using AltenBooking.Domain.Entities;
using AltenBooking.Web.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace AltenBooking.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;

        public ProfileController(UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _userManager = userManager;
            _environment = environment;
        }

        public async Task<IActionResult> Detail()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = _userManager.GetRolesAsync(user).Result;

            var model = new ProfileModelDTO()
            {
                UserId = user.Id,
                Username = user.UserName,
                UserRating = user.Rating.ToString(),
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                DateJoined = user.MemberSince,
                IsActive = user.IsActive,
                IsAdmin = userRoles.Contains("Admin")
            };

            return View(model);
        }
    }
}
