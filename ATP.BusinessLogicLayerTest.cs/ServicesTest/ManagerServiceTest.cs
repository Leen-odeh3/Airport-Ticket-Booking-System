﻿using ATP.BusinessLogicLayer.Models;
using ATP.BusinessLogicLayer.Services;


namespace ATP.BusinessLogicLayerTest.ServicesTest
{
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

            // Initialize ManagerService with mock dependencies
            _managerService = new ManagerService(flights);
        }

        [Fact]
        public void FilterByFlight_ValidInput_ReturnsFilteredFlights()
        {
            // Arrange
            var flights = new List<FlightDomainModel>
    {
        new FlightDomainModel(1, 100.0, "CountryA", "CountryB", DateTime.Now, "AirportA", "AirportB", FlightClass.Economy),
        new FlightDomainModel(2, 150.0, "CountryA", "CountryC", DateTime.Now, "AirportA", "AirportC", FlightClass.Business),
        new FlightDomainModel(3, 200.0, "CountryD", "CountryB", DateTime.Now, "AirportD", "AirportB", FlightClass.First)
    };
            var managerService = new ManagerService(flights);

            // Act
            var filteredFlights = managerService.FilterByFlight("CountryA", "CountryB");

            // Assert
            Assert.Single(filteredFlights);
            Assert.Equal(1, filteredFlights[0].Id);
        }

        [Fact]
        public void FilterByPrice_ValidInput_ReturnsFilteredFlights()
        {
            // Arrange
            var flights = new List<FlightDomainModel>
    {
        new FlightDomainModel(1, 100.0, "CountryA", "CountryB", DateTime.Now, "AirportA", "AirportB", FlightClass.Economy),
        new FlightDomainModel(2, 150.0, "CountryA", "CountryC", DateTime.Now, "AirportA", "AirportC", FlightClass.Business),
        new FlightDomainModel(3, 200.0, "CountryD", "CountryB", DateTime.Now, "AirportD", "AirportB", FlightClass.First)
    };
            var managerService = new ManagerService(flights);

            // Act
            var filteredFlights = managerService.FilterByPrice(100.0, 150.0);

            // Assert
            Assert.Equal(2, filteredFlights.Count);
            Assert.Contains(filteredFlights, f => f.Id == 1);
            Assert.Contains(filteredFlights, f => f.Id == 2);
        }

        [Fact]
        public void FilterByPrice_NoMatchingFlights_ReturnsEmptyList()
        {
            // Arrange
            var flights = new List<FlightDomainModel>
    {
        new FlightDomainModel(1, 100.0, "CountryA", "CountryB", DateTime.Now, "AirportA", "AirportB", FlightClass.Economy),
        new FlightDomainModel(2, 150.0, "CountryA", "CountryC", DateTime.Now, "AirportA", "AirportC", FlightClass.Business),
        new FlightDomainModel(3, 200.0, "CountryD", "CountryB", DateTime.Now, "AirportD", "AirportB", FlightClass.First)
    };
            var managerService = new ManagerService(flights);

            // Act
            var filteredFlights = managerService.FilterByPrice(300.0, 400.0);

            // Assert
            Assert.Empty(filteredFlights);
        }



    }
}
