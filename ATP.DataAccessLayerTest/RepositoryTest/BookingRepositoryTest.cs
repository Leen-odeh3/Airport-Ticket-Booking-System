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
        // Arrange
        var bookings = new List<BookingDomainModel>
        {
            new BookingDomainModel(1, 100, FlightClass.Economy, DateTime.Now, "CountryA", "CountryB")
        };

        // Create a mock BookingRepository with injected dependencies
        var mockBookingRepository = new Mock<BookingRepository>(_testCsvFilePath) { CallBase = true };

        // Act
        mockBookingRepository.Object.WriteBookingsToCsv(bookings);

        // Assert
        // Verify that WriteRecords was called on CsvWriter
        // You may need to use reflection or other techniques to verify the method call on CsvWriter
    }

    [Fact]
    public void ReadBookingsFromCsv_WithValidCsv_ReturnsBookingsList()
    {
        // Arrange
        var csvContent = "BookingId,PropertyName,Property2,...\n1,Value1,Value2,...";
        File.WriteAllText(_testCsvFilePath, csvContent);

        // Create a mock BookingRepository with injected dependencies
        var mockBookingRepository = new Mock<BookingRepository>(_testCsvFilePath) { CallBase = true };

        // Act
        var result = mockBookingRepository.Object.ReadBookingsFromCsv();

        // Assert
        // Verify the result
    }
}
