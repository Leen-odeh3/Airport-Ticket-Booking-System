using ATP.BusinessLogicLayer.DTOs;
using Microsoft.Extensions.Logging;

public class BookingService
{
    private List<BookingDTO> bookings;
    private int nextBookingId;
    private readonly ILogger<BookingService> _logger;

    public BookingService(ILogger<BookingService> logger)
    {
        bookings = new List<BookingDTO>();
        nextBookingId = 1;
        _logger = logger;
    }

    public void BookFlight(FlightDto flight)
    {
        int bookingId = nextBookingId++;
        var booking = new BookingDTO(bookingId, flight.Id, flight.Class.ClassName, DateTime.Now);
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

    public List<BookingDTO> GetBookings()
    {
        return bookings;
    }

    public BookingDTO ViewPersonalBookingDetails(int bookingId)
    {
        return bookings.Find(b => b.BookingId == bookingId);
    }

    public List<BookingDTO> FilterBookings(Func<BookingDTO, bool> predicate)
    {
        return bookings.Where(predicate).ToList();
    }
}
