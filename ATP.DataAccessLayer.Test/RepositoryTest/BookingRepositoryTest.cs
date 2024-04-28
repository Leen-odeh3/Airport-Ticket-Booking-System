using ATP.DataAccessLayer.Repository;
using ATP.BusinessLogicLayer.Models;
using CsvHelper;
using Moq;
using System.Globalization;

namespace ATP.DataAccessLayerTest.RepositoryTest
{
    public class BookingRepositoryTest
    {
        private static Mock<StreamReader> _streamReaderMock;
        private static Mock<CsvReader> _csvReaderMock;

        [Fact]
        public void ReadBookings_FromCsv_Success()
        {
            // Arrange
            var csvFilePath = "test.csv";
            var csvData = "Id,FlightId,FlightClass,BookingDate,DepartureCountry,DestinationCountry\n1,1,Economy,2024-04-25T12:00:00Z,USA,UK\n";
            File.WriteAllText(csvFilePath, csvData);

            _streamReaderMock = new Mock<StreamReader>(csvFilePath);
            _csvReaderMock = new Mock<CsvReader>(_streamReaderMock.Object, CultureInfo.InvariantCulture);

            _csvReaderMock.Setup(csvReader => csvReader.GetRecords<BookingDomainModel>())
                         .Returns(new List<BookingDomainModel>());

            var bookingRepository = new BookingRepository(csvFilePath);

            // Act
            var result = bookingRepository.ReadBookingsFromCsv();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
