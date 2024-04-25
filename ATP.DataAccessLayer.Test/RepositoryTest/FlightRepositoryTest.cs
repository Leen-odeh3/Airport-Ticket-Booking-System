using ATP.BusinessLogicLayer.Models;
using ATP.DataAccessLayer.Repository;
using Moq;

namespace ATP.DataAccessLayerTest.RepositoryTest
{
    public class FlightRepositoryTest
    {
        private readonly Mock<IFlightRepository> _repositoryMock = new Mock<IFlightRepository>();
        private readonly List<FlightDomainModel> _flights;

        public FlightRepositoryTest()
        {
            _flights = new List<FlightDomainModel>
            {
                new FlightDomainModel(
                    id: 1,
                    price: 100,
                    departureCountry: "USA",
                    destinationCountry: "UK",
                    departureDate: DateTime.Now,
                    departureAirport: "JFK",
                    arrivalAirport: "LHR",
                    flightClass: FlightClass.Economy
                ),
                new FlightDomainModel(
                    id: 2,
                    price: 200,
                    departureCountry: "UK",
                    destinationCountry: "France",
                    departureDate: DateTime.Now,
                    departureAirport: "LHR",
                    arrivalAirport: "CDG",
                    flightClass: FlightClass.Business
                )
            };
        }

        [Fact]
        public void GetById_ReturnsFlightWithMatchingId()
        {
            // Arrange
            int idToFind = 1;
            _repositoryMock.Setup(repo => repo.GetById(idToFind)).Returns(_flights[0]);

            var retrievedFlight = _repositoryMock.Object.GetById(idToFind);

            Assert.NotNull(retrievedFlight);
            Assert.Equal(idToFind, retrievedFlight.Id);
        }
    }

    public interface IFlightRepository
    {
        FlightDomainModel GetById(int id);
    }
}
