using AltenBooking.Web.Models.DTO;
using FluentValidation;
using System;

namespace AltenBooking.Web.Validators
{
    public class BookingDTOValidator : AbstractValidator<BookingDTO>
    {
        public BookingDTOValidator()
        {
            RuleFor(x => x.Room)
                .NotNull().WithMessage("Please select the room");

            RuleFor(x => x.NumberGuests)
                .NotEmpty().WithMessage("Please inform the number of guests")
                .LessThan(5).WithMessage("We only have 1 room with capacity for 4 people");

            RuleFor(x => x.ArriveDate)
                .NotEmpty().WithMessage("Arrive date is required")
                .LessThan(m => m.OutDate).WithMessage("Start date can not be higher than end date.")
                .GreaterThan(q => q.OutDate.Value.Date.AddMonths(-1))
                   .WithMessage("Cannot be booked more than 30 days in advance");


            RuleFor(x => x.OutDate)
                .NotEmpty().WithMessage("Exit date is required")
                 .GreaterThan(x => x.ArriveDate)
                   .WithMessage("End date cannot be less than start date")
                .LessThan(x => x.ArriveDate.Value.Date.AddDays(4))
                   .WithMessage("The stay cannot be longer than 3 days");              

        }
       
    }
}
