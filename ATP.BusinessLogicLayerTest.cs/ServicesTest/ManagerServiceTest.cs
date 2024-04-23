using ATP.BusinessLogicLayer.Models;
using ATP.BusinessLogicLayer.Services;
namespace ATP.BusinessLogicLayerTest.ServicesTest;

public class ManagerServiceTest
{
    private readonly ManagerService _managerService;
    public ManagerServiceTest()
    {
        var flights = new List<FlightDomainModel>
        {
            new FlightDomainModel(1, 100.0, "CountryA", "CountryB", DateTime.Now, "AirportA", "AirportB", FlightClass.Economy),
            new FlightDomainModel(2, 150.0, "CountryA", "CountryC", DateTime.Now, "AirportA", "AirportC", FlightClass.Business),
            new FlightDomainModel(3, 200.0, "CountryD", "CountryB", DateTime.Now, "AirportD", "AirportB", FlightClass.First)
        };
        _managerService = new ManagerService(flights);
    }

   

    [Fact]
    public void FilterByFlight_ValidInput_ReturnsFilteredFlights()
    {
        // Act
        var filteredFlights = _managerService.FilterByFlight("CountryA", "CountryB");

        // Assert
        Assert.Single(filteredFlights);
        Assert.Equal(1, filteredFlights[0].Id);

        // Test case: Departure country not found
        var filteredFlightsDepartureNotFound = _managerService.FilterByFlight("InvalidCountry", "CountryB");
        Assert.Empty(filteredFlightsDepartureNotFound);

        // Test case: Destination country not found
        var filteredFlightsDestinationNotFound = _managerService.FilterByFlight("CountryA", "InvalidCountry");
        Assert.Empty(filteredFlightsDestinationNotFound);

        // Test case: Case-insensitive matching
        var filteredFlightsCaseInsensitive = _managerService.FilterByFlight("countrya", "countryb");
        Assert.Single(filteredFlightsCaseInsensitive);
        Assert.Equal(1, filteredFlightsCaseInsensitive[0].Id);
    }


    [Fact]
    public void FilterByPrice_ValidInput_ReturnsFilteredFlights()
    {
        // Act
        var filteredFlights = _managerService.FilterByPrice(100.0, 150.0);

        // Assert
        Assert.Equal(2, filteredFlights.Count);
        Assert.Contains(filteredFlights, f => f.Id == 1);
        Assert.Contains(filteredFlights, f => f.Id == 2);
    }

    [Fact]
    public void FilterByPrice_NoMatchingFlights_ReturnsEmptyList()
    {
        // Act
        var filteredFlights = _managerService.FilterByPrice(300.0, 400.0);

        // Assert
        Assert.Empty(filteredFlights);
    }

    [Fact]
    public void FilterByClass_ValidInput_ReturnsFilteredFlights()
    {
        // Act
        var filteredFlights = _managerService.FilterByClass(FlightClass.Economy);

        // Assert
        Assert.Single(filteredFlights);
        Assert.Equal(1, filteredFlights[0].Id);
    }
}
