using ATP.BusinessLogicLayer.Models;
using CsvHelper;
using Microsoft.Extensions.Logging;
using System.Globalization;

public class BookingService
{
    private List<BookingDomainModel> bookings;
    private int nextBookingId;
    private readonly ILogger<BookingService> _logger;
    private readonly string _csvFilePath;

    public BookingService(string csvFilePath, ILogger<BookingService> logger)
    {
        bookings = new List<BookingDomainModel>();
        nextBookingId = 1;
        _logger = logger;
        _csvFilePath = csvFilePath;
    }

    public void BookFlight(FlightDomainModel flight)
    {
        var booking = new BookingDomainModel(flight.Id, flight.Id, flight.Class, flight.DepartureDate, flight.DepartureCountry, flight.DestinationCountry);
        bookings.Add(booking);
        WriteBookingToCsv(booking);
        _logger.LogInformation($"Booking with ID {flight.Id} successfully created for the flight from {flight.DepartureCountry} to {flight.DestinationCountry} on {flight.DepartureDate}.");
    }


    private void WriteBookingToCsv(BookingDomainModel booking)
    {
        try
        {
            using (var writer = new StreamWriter(_csvFilePath, append: true))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecord(booking);
                csv.NextRecord();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while writing booking to CSV file: {ex.Message}");
        }
    }


    public void CancelBooking(int bookingId)
    {
        var booking = bookings.Find(b => b.BookingId == bookingId);
        if (booking is not null)
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
