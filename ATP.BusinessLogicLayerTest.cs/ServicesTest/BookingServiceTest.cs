using ATP.BusinessLogicLayer.Models;
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
    public void BookFlight_ValidFlightParameters_BooksSuccessfully()
    {
        // Arrange
        var validFlight = CreateFlight(1, "CountryA", "CountryB", FlightClass.Economy);
        // Act
        _bookingService.BookFlight(validFlight);

        // Assert
        var bookings = _bookingService.GetBookings();
        Assert.Single(bookings);
        Assert.Equal(validFlight.Id, bookings[0].FlightId);
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

    [Fact]
    public void BookFlight_InvalidFlightParameters_ThrowsException()
    {
        // Arrange
        var invalidFlight = CreateFlight(0, "Invalid", "Invalid", FlightClass.Economy);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _bookingService.BookFlight(invalidFlight));

        // Assert
        Assert.Empty(_bookingService.GetBookings());
    }

    [Fact]
    public void CancelBooking_NonExistingBooking_DoesNotRemoveAnyBooking()
    {
        // Arrange
        var flight1 = CreateFlight(1, "CountryA", "CountryB", FlightClass.Economy);
        var flight2 = CreateFlight(2, "CountryC", "CountryD", FlightClass.Business);
        _bookingService.BookFlight(flight1);
        _bookingService.BookFlight(flight2);
        var initialBookingsCount = _bookingService.GetBookings().Count;

        // Act
        _bookingService.CancelBooking(999);

        // Assert
        var finalBookingsCount = _bookingService.GetBookings().Count;
        Assert.Equal(initialBookingsCount, finalBookingsCount);
    }

    [Fact]
    public void ViewPersonalBookingDetails_ExistingBooking_ReturnsCorrectBooking()
    {
        // Arrange
        var flight = CreateFlight(1, "CountryA", "CountryB", FlightClass.Economy);
        _bookingService.BookFlight(flight);

        // Act
        var bookingDetails = _bookingService.ViewPersonalBookingDetails(1);

        // Assert
        Assert.NotNull(bookingDetails);
        Assert.Equal(1, bookingDetails.BookingId);
    }

    private FlightDomainModel CreateFlight(int id, string departureCountry, string destinationCountry, FlightClass flightClass)
    {
        return new FlightDomainModel(id, 100.0, departureCountry, destinationCountry, DateTime.Now, "AirportA", "AirportB", flightClass);
    }
}
