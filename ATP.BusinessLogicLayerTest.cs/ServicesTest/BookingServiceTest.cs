using ATP.BusinessLogicLayer.Models;
using CsvHelper;
using Microsoft.Extensions.Logging;
using Moq;

namespace ATP.BusinessLogicLayerTest.ServicesTest;

public class BookingServiceTest 
{
    private readonly BookingService _bookingService;
    private readonly Mock<ILogger<BookingService>> _loggerMock;

    public BookingServiceTest()
    {
        _loggerMock = new Mock<ILogger<BookingService>>();
        _bookingService = new BookingService("test.csv", _loggerMock.Object);
    }

    [Fact]
    public void CancelBooking_ExistingBooking_CancelsSuccessfully()
    {
        // Arrange
        var flight = CreateFlight(1, "CountryA", "CountryB", FlightClass.Economy);

        // Act
        _bookingService.BookFlight(flight);
        _bookingService.CancelBooking(1);

        // Assert
        Assert.Empty(_bookingService.GetBookings());
    }

    [Fact]
    public void GetAllBookings_ReturnsAllBookings()
    {
        // Arrange
        var flight1 = CreateFlight(1, "CountryA", "CountryB", FlightClass.Economy);
        var flight2 = CreateFlight(2, "CountryC", "CountryD", FlightClass.Business);

        // Act
        _bookingService.BookFlight(flight1);
        _bookingService.BookFlight(flight2);

        // Assert
        var bookings = _bookingService.GetBookings();
        Assert.Equal(2, bookings.Count);
        Assert.Contains(bookings, b => b.FlightId == flight1.Id);
        Assert.Contains(bookings, b => b.FlightId == flight2.Id);
    }

    [Fact]
    public void AddBooking_PassengerHasNotBookedThisFlightBefore_ReturnsSuccess()
    {
        // Arrange
        var flight = CreateFlight(1, "CountryA", "CountryB", FlightClass.Economy);

        // Act
        _bookingService.BookFlight(flight);

        // Assert
        var bookings = _bookingService.GetBookings();
        Assert.Equal(1, bookings.Count);

        // Add the same booking again
        _bookingService.BookFlight(flight);

        // Assert
        bookings = _bookingService.GetBookings();
        Assert.Equal(2, bookings.Count);
    }

    [Fact]
    public void FilterBookings_ReturnsFilteredBookings()
    {
        // Arrange
        var flight1 = CreateFlight(1, "CountryA", "CountryB", FlightClass.Economy);
        var flight2 = CreateFlight(2, "CountryC", "CountryD", FlightClass.Business);
        var flight3 = CreateFlight(3, "CountryE", "CountryF", FlightClass.First);

        _bookingService.BookFlight(flight1);
        _bookingService.BookFlight(flight2);
        _bookingService.BookFlight(flight3);

        // Act
        var filteredBookings = _bookingService.FilterBookings(b => b.FlightClass == FlightClass.Economy);

        // Assert
        Assert.Single(filteredBookings);
        Assert.Equal(flight1.Id, filteredBookings[0].FlightId);
    }

    private FlightDomainModel CreateFlight(int id, string departureCountry, string destinationCountry, FlightClass flightClass)
    {
        return new FlightDomainModel(id, 100.0, departureCountry, destinationCountry, DateTime.Now, "AirportA", "AirportB", flightClass);
    }
}



