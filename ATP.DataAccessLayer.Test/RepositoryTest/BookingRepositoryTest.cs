using ATP.DataAccessLayer.Repository;
using ATP.BusinessLogicLayer.Models;
using CsvHelper;
using Moq;
using System.Globalization;


namespace ATP.DataAccessLayerTest.RepositoryTest;
public class BookingRepositoryTest
{
    [Fact]
    public void ReadBookings_FromCsv_Success()
    {
        // Arrange
        var csvFilePath = "test.csv";
        var csvData = "Id,FlightId,FlightClass,BookingDate,DepartureCountry,DestinationCountry\n1,1,Economy,2024-04-25T12:00:00Z,USA,UK\n";
        File.WriteAllText(csvFilePath, csvData);

        var streamReaderMock = new Mock<StreamReader>(csvFilePath);
        var csvReaderMock = new Mock<CsvReader>(streamReaderMock.Object, CultureInfo.InvariantCulture);

        csvReaderMock.Setup(csvReader => csvReader.GetRecords<BookingDomainModel>())
                     .Returns(new List<BookingDomainModel>());

        var bookingRepository = new BookingRepository(csvFilePath);

        // Act
        var result = bookingRepository.ReadBookingsFromCsv();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
