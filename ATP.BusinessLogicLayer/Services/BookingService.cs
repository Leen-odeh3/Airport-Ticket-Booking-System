using ATP.DataAccessLayer.Models;

public class BookingService
{
    private List<Booking> bookings;
    private int nextBookingId;

    public BookingService()
    {
        bookings = new List<Booking>();
        nextBookingId = 1;
    }
    public void BookFlight(Flight flight)
    {
        int bookingId = nextBookingId++;
        var booking = new Booking
        {
            BookingId = bookingId,
            Flight = flight
        };
        bookings.Add(booking);
        Console.WriteLine($"Booking with ID {bookingId} successfully created for the flight from {flight.DepartureCountry} to {flight.DestinationCountry} on {flight.DepartureDate}.");
    }
    public void CancelBooking(int bookingId)
    {
       
        foreach (var b in bookings) 
        {
            Console.WriteLine($"Booking ID: {b.BookingId}");
        }

        var booking = bookings.Find(b => b.BookingId == bookingId);
        if (booking is not null)
        {
            bookings.Remove(booking);
            Console.WriteLine("Booking canceled successfully.");
        }
        else
        {
            Console.WriteLine("Booking not found.");
        }
    }
    public void ViewPersonalBookingDetails(int bookingId)
    {
        var booking = bookings.Find(b => b.BookingId == bookingId);
        if (booking is not null)
        { 
            Console.WriteLine($"Booking ID: {booking.BookingId}");
            Console.WriteLine($"Flight Details:");
            Console.WriteLine($"   Departure: {booking.Flight.DepartureCountry}");
            Console.WriteLine($"   Destination: {booking.Flight.DestinationCountry}");
            Console.WriteLine($"   Date: {booking.Flight.DepartureDate}");
            Console.WriteLine($"   Class: {booking.Flight.Class}");
        }
        else
        {
            Console.WriteLine("Booking not found.");
        }
    }
    public List<Booking> FilterBookings(Func<Booking, bool> predicate)
    {
        return bookings.Where(predicate).ToList();
    }

}
