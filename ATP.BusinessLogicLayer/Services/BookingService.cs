using ATP.DataAccessLayer.Models;

namespace ATP.BusinessLogicLayer.Services;
public class BookingService
{
    private List<Booking> _bookings;

    public BookingService()
    {
        _bookings = new List<Booking>();
    }
    public Booking GetBookingById(int bookingId)
    {
        return _bookings.FirstOrDefault(b => b.BookingId == bookingId);
    }

    public void AddBooking(Booking booking)
    {
        _bookings.Add(booking);
    }

    public void CancelBooking(int bookingId)
    {
        var booking = _bookings.Find(b => b.BookingId == bookingId);
        if (booking is not null)
        {
            _bookings.Remove(booking);
            Console.WriteLine($"Booking ID {bookingId} has been canceled successfully.");
        }
        else
        {
            Console.WriteLine($"Booking with ID {bookingId} does not exist.");
        }
    }

    public void ViewBookingDetails(int bookingId)
    {
        var booking = _bookings.Find(b => b.BookingId == bookingId);
        if (booking is not null)
        {
            Console.WriteLine($"Booking ID: {booking.BookingId}");
            Console.WriteLine($"Passenger ID: {booking.PassengerId}");
            Console.WriteLine($"Flight ID: {booking.FlightId}");
            Console.WriteLine($"Flight Class: {booking.FlightClass}");
            Console.WriteLine($"Booking Date: {booking.BookingDate}");
        }
        else
        {
            Console.WriteLine($"Booking with ID {bookingId} does not exist.");
        }
    }

    public void ModifyBooking(int bookingId, Booking newBooking)
    {
        var booking = _bookings.Find(b => b.BookingId == bookingId);
        if (booking != null)
        {
            booking.FlightId = newBooking.FlightId;
            booking.FlightClass = newBooking.FlightClass;
            booking.BookingDate = newBooking.BookingDate;
            Console.WriteLine($"Booking ID {bookingId} has been modified successfully.");
        }
        else
        {
            Console.WriteLine($"Booking with ID {bookingId} does not exist.");
        }
    }

    public List<Booking> GetPassengerBookings(Passenger passenger)
    {
        return _bookings.FindAll(b => b.PassengerId == passenger.PassengerId);
    }

    public List<Booking> GetAllBookings()
    {
        return _bookings;
    }
}
