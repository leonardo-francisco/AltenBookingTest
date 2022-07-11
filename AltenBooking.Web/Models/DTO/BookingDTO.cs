using System;
using System.ComponentModel.DataAnnotations;

namespace AltenBooking.Web.Models.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public string Room { get; set; }
        public int NumberGuests { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? ArriveDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? OutDate { get; set; }
        [Required]
        public string Request { get; set; }
        public DateTime Created { get; set; }

        public string UserId { get; set; }
    }
}
