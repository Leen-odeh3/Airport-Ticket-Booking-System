using ATP.BusinessLogicLayer.Models;
using Microsoft.Extensions.Logging;

public class BookingService
{
    private List<BookingDomainModel> bookings;
    private int nextBookingId;
    private readonly ILogger<BookingService> _logger;

    public BookingService(ILogger<BookingService> logger)
    {
        bookings = new List<BookingDomainModel>();
        nextBookingId = 1;
        _logger = logger;
    }

    public void BookFlight(FlightDomainModel flight)
    {
        int bookingId = nextBookingId++;
        var booking = new BookingDomainModel(bookingId, flight.Id, flight.Class, DateTime.Now, flight.DepartureCountry, flight.DestinationCountry);
        bookings.Add(booking);
        _logger.LogInformation($"Booking with ID {bookingId} successfully created for the flight from {flight.DepartureCountry} to {flight.DestinationCountry} on {flight.DepartureDate}.");
    }


    public void CancelBooking(int bookingId)
    {
        var booking = bookings.Find(b => b.BookingId == bookingId);
        if (booking != null)
        {
            bookings.Remove(booking);
            _logger.LogInformation("Booking canceled successfully.");
        }
        else
        {
            _logger.LogInformation("Booking not found.");
        }
    }

    public List<BookingDomainModel> GetBookings()
    {
        return bookings;
    }

    public BookingDomainModel ViewPersonalBookingDetails(int bookingId)
    {
        return bookings.FirstOrDefault(b => b.BookingId == bookingId);
    }

    public List<BookingDomainModel> FilterBookings(Func<BookingDomainModel, bool> predicate)
    {
        return bookings.Where(predicate).ToList();
    }

}
