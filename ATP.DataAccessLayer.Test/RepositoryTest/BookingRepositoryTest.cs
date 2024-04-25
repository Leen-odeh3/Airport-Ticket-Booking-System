using ATP.BusinessLogicLayer.Models;
using ATP.DataAccessLayer.Repository;
using Moq;

namespace ATP.DataAccessLayerTest.RepositoryTest;

public class BookingRepositoryTest
{
    private readonly string _testCsvFilePath = "test.csv";

    [Fact]
    public void WriteBookingsToCsv_WithValidBookings_Succeeds()
    {
        var bookings = new List<BookingDomainModel>
{
    new BookingDomainModel(1, 100, FlightClass.Economy, new DateTime(2024, 03, 10), "USA", "UK"),
    new BookingDomainModel(2, 500, FlightClass.Business, new DateTime(2024, 03, 15), "UK", "France"),
    new BookingDomainModel(3, 300, FlightClass.First, new DateTime(2024, 03, 20), "Germany", "Italy"),
};

        var mockBookingRepository = new Mock<BookingRepository>(_testCsvFilePath) { CallBase = true };

        mockBookingRepository.Object.WriteBookingsToCsv(bookings);

        var expectedLines = new List<string>
            {
                "1,100,Economy,3/10/2024,USA,UK",
                "2,500,Business,3/15/2024,UK,France",
                "3,300,First,3/20/2024,Germany,Italy"
            };

        var actualLines = File.ReadAllLines(_testCsvFilePath);
        Assert.Equal(expectedLines, actualLines);
    }


}
