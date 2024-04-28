using ATP.BusinessLogicLayer.Models;
using ATP.DataAccessLayer.Mapper;
using ATP.DataAccessLayer.Models;
using ATP.DataAccessLayer.Repository;
using Microsoft.Extensions.Logging;
using Moq;

namespace ATP.DataAccessLayerTest.RepositoryTest;

public class FlightRepositoryTest
{
    private readonly Mock<FlightMapper> _mapperMock;
    private readonly Mock<ILogger<FlightRepository>> _loggerMock;
    private readonly List<FlightDomainModel> _flights;

    public FlightRepositoryTest()
    {
        _mapperMock = new Mock<FlightMapper>();
        _loggerMock = new Mock<ILogger<FlightRepository>>();

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
        int idToFind = 1;
        _mapperMock.Setup(m => m.MapToDomain(It.IsAny<Flight>()))
            .Returns((Flight f) => _flights.Find(x => x.Id == f.Id));

        var repository = new FlightRepository("dummy.csv", _mapperMock.Object, _loggerMock.Object);
        var retrievedFlight = repository.GetById(idToFind);

        Assert.NotNull(retrievedFlight);
        Assert.Equal(idToFind, retrievedFlight.Id);
    }

    [Fact]
    public void GetAll_ReturnsAllFlights()
    {
        _mapperMock.Setup(m => m.MapToDomain(It.IsAny<Flight>()))
            .Returns((Flight f) => _flights.Find(x => x.Id == f.Id));

        var repository = new FlightRepository("dummy.csv", _mapperMock.Object, _loggerMock.Object);
        var allFlights = repository.GetAll();

        Assert.Equal(_flights.Count, allFlights.Count);
        Assert.Equal(_flights, allFlights);
    }

    [Fact]
    public void Update_UpdatesExistingFlight()
    {
        var flightToUpdate = _flights[0];
        flightToUpdate.Price = 150;

        _mapperMock.Setup(m => m.MapToDomain(It.IsAny<Flight>()))
            .Returns((Flight f) => _flights.Find(x => x.Id == f.Id));

        var repository = new FlightRepository("dummy.csv", _mapperMock.Object, _loggerMock.Object);
        repository.Update(flightToUpdate);

        var updatedFlight = repository.GetById(flightToUpdate.Id);
        Assert.NotNull(updatedFlight);
        Assert.Equal(150, updatedFlight.Price);
    }
}
