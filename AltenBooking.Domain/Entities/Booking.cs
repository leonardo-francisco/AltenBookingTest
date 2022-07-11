using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltenBooking.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string Room { get; set; }
        public int NumberGuests { get; set; }
        public DateTime? ArriveDate { get; set; }
        public DateTime? OutDate { get; set; }
        public string Request { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
