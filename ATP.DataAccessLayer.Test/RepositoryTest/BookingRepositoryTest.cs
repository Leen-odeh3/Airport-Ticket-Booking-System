using ATP.BusinessLogicLayer.Models;
using ATP.DataAccessLayer.Repository;
using CsvHelper;
using Moq;
using System.Globalization;


namespace ATP.DataAccessLayerTest.RepositoryTest;

public class BookingRepositoryTest
{
    private readonly string _csvFilePath = "test.csv";
    private readonly Mock<CsvReader> _csvReaderMock;
    private readonly BookingRepository _bookingRepository;

    public BookingRepositoryTest()
    {
        _csvReaderMock = new Mock<CsvReader>(new StreamReader(_csvFilePath), CultureInfo.InvariantCulture);
        _bookingRepository = new BookingRepository(_csvFilePath);
    }

    [Fact]
    public void ReadBookings_FromCsv_Success()
    {
        // Arrange
        var csvData = "Id,FlightId,FlightClass,BookingDate,DepartureCountry,DestinationCountry\n1,1,Economy,2024-04-25T12:00:00Z,USA,UK\n";
        File.WriteAllText(_csvFilePath, csvData);

        _csvReaderMock.Setup(csvReader => csvReader.GetRecords<BookingDomainModel>())
                      .Returns(new List<BookingDomainModel>());

        // Act
        var result = _bookingRepository.ReadBookingsFromCsv();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
