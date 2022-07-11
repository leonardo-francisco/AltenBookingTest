# AltenTest
Booking Cancun Last Hotel
## Usage
```bash
A short guide how to run it
```
1. Home Page - User can check a possible checkin and checkout date(datepicker calendar).
The click event on the button after selecting the dates will select directly from the API
if there is a date available or not
2. Validation message will indicate that for the user to finish the reservation he must
register(if not registered) or log in
3. When loggin user will be on the Home Page but the button will indicate now the path do make 
the reservation
4. Page to create the reservation (CreateBooking) at this moment the user must select the
desired dates and fill the other fileds. There will be launch the validation of dates
according to the API
5. If user want to change or cancel the reservation, the user must click on the gear icon
(navigation bar) and choose the option to check MyReservations
6. The Index Page of the Booking Controller will show the lists of reservations made according
to the user login where use can select whether to change(EditBooking) or cancel(DeleteBooking)
7. The reservation change screen follows the same validation rule as the reservation creation 
screen. Both screens have FluentValidation to apply the rules

