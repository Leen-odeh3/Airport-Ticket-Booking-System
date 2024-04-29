using Moq;
using Microsoft.Extensions.Logging;
using ATP.BusinessLogicLayer.Models;
using AutoFixture;

namespace ATP.BusinessLogicLayer.Test.ServicesTest;

public class BookingServiceTest 
{
    private BookingService _service;
    private Mock<ILogger<BookingService>> _loggerMock;
    private string _csvFilePath;
    private IFixture _fixture;

    public BookingServiceTest()
    {
        _fixture = new Fixture();
        _loggerMock = _fixture.Freeze<Mock<ILogger<BookingService>>>();
        _csvFilePath = "test.csv";
        _service = new BookingService(_csvFilePath, _loggerMock.Object);
    }


    [Fact]
    public void BookFlight_AddsBooking_Successfully()
    {
        var flight = _fixture.Create<FlightDomainModel>();

        _service.BookFlight(flight);

        Assert.Single(_service.GetBookings());
    }

    [Fact]
    public void BookFlight_Logs_Information()
    {
     
        var flight = _fixture.Create<FlightDomainModel>();
        string expectedMessage = $"Booking with ID {flight.Id} successfully created for the flight from {flight.DepartureCountry} to {flight.DestinationCountry} on {flight.DepartureDate}.";

        _service.BookFlight(flight);

        _loggerMock.Verify(
            logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => string.Equals(expectedMessage, v.ToString(), StringComparison.OrdinalIgnoreCase)),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
            Times.Once);
    }

    [Fact]
    public void CancelBooking_RemovesBooking_Successfully()
    {
        var flight = _fixture.Create<FlightDomainModel>();
        _service.BookFlight(flight);
        int bookingId = flight.Id;

        _service.CancelBooking(bookingId);

        Assert.Empty(_service.GetBookings());
    }

    [Fact]
    public void GetBookings_Returns_Bookings_List()
    {
        var flight1 = _fixture.Create<FlightDomainModel>();
        var flight2 = _fixture.Create<FlightDomainModel>();
        _service.BookFlight(flight1);
        _service.BookFlight(flight2);

        var bookings = _service.GetBookings();

        Assert.Equal(2, bookings.Count);
        Assert.Contains(bookings, booking => booking.FlightId == flight1.Id);
        Assert.Contains(bookings, booking => booking.FlightId == flight2.Id);
    }

    [Fact]
    public void ViewPersonalBookingDetails_Returns_CorrectBookingDetails()
    {
        // Arrange
        var flight = _fixture.Create<FlightDomainModel>();
        _service.BookFlight(flight);
        int bookingId = flight.Id;

        // Act
        var booking = _service.ViewPersonalBookingDetails(bookingId);

        // Assert
        Assert.NotNull(booking);
        Assert.Equal(bookingId, booking.BookingId);
    }

    [Fact]
    public void ViewPersonalBookingDetails_ReturnsNull_ForNonexistentBooking_Id()
    {

        int nonExistentBookingId = 999; 

        var booking = _service.ViewPersonalBookingDetails(nonExistentBookingId);

        Assert.Null(booking);
    }

    [Theory]
    [InlineData(FlightClass.Economy)]
    [InlineData(FlightClass.Business)]
    [InlineData(FlightClass.First)]
    public void FilterBookings_ReturnsFiltered_BookingsList(FlightClass flightClass)
    {
        // Arrange
        var flight1 = _fixture.Create<FlightDomainModel>();
        var flight2 = _fixture.Create<FlightDomainModel>();
        flight1.Class = flightClass;
        flight2.Class = flightClass;
        _service.BookFlight(flight1);
        _service.BookFlight(flight2);

        var filteredBookings = _service.FilterBookings(booking => booking.FlightClass == flightClass);

        Assert.NotNull(filteredBookings);
        Assert.NotEmpty(filteredBookings);
        Assert.True(filteredBookings.All(b => b.FlightClass == flightClass));
    }

}
