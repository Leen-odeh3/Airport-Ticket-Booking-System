using ATP.DataAccessLayer.Models;
using ATP.DataAccessLayer.Repository;
using ATP.DataAccessLayer.Mapper;
using AutoFixture;
using AutoFixture.AutoMoq;
using CsvHelper;
using Microsoft.Extensions.Logging.Abstractions;
using ATP.BusinessLogicLayer.Models;
using System.Globalization;

namespace ATP.DataAccessLayer.Test;

public class FlightRepositoryTest
{
    private readonly Fixture _fixture;
    private readonly string _csvFilePath;

    public FlightRepositoryTest()
    {
        _fixture = new Fixture();
        _fixture.Customize(new AutoMoqCustomization());
        _csvFilePath = "test_flights.csv";
    }

    [Fact]
    public void GetById_ExistingId_ReturnsFlight()
    {
        // Arrange
        var repository = CreateFlightRepositoryWithTestData();
        var expectedFlight = repository.GetAll().First();

        // Act
        var actualFlight = repository.GetById(expectedFlight.Id);

        // Assert
        Assert.Equal(expectedFlight, actualFlight);
    }

    [Fact]
    public void GetById_NonExistingId_ReturnsNull()
    {
        // Arrange
        var repository = CreateFlightRepositoryWithTestData();
        var nonExistingId = repository.GetAll().Max(f => f.Id) + 1;

        // Act
        var flight = repository.GetById(nonExistingId);

        // Assert
        Assert.Null(flight);
    }

    [Fact]
    public void Add_NewFlight_FlightAdded()
    {
        // Arrange
        var repository = CreateFlightRepositoryWithTestData();
        var newFlight = _fixture.Create<FlightDomainModel>();

        // Act
        repository.Add(newFlight);
        var addedFlight = repository.GetById(newFlight.Id);

        // Assert
        Assert.NotNull(addedFlight);
        Assert.Equal(newFlight, addedFlight);
    }

    [Fact]
    public void Delete_ExistingFlight_FlightRemoved()
    {
        // Arrange
        var repository = CreateFlightRepositoryWithTestData();
        var flightToDelete = repository.GetAll().First();

        // Act
        repository.Delete(flightToDelete);
        var deletedFlight = repository.GetById(flightToDelete.Id);

        // Assert
        Assert.Null(deletedFlight);
    }

    [Fact]
    public void Update_ExistingFlight_FlightUpdated()
    {
        // Arrange
        var repository = CreateFlightRepositoryWithTestData();
        var flightToUpdate = repository.GetAll().First();
        var updatedFlight = _fixture.Create<FlightDomainModel>();
        updatedFlight.Id = flightToUpdate.Id;

        // Act
        repository.Update(updatedFlight);
        var retrievedFlight = repository.GetById(updatedFlight.Id);

        // Assert
        Assert.Equal(updatedFlight, retrievedFlight);
    }

    private FlightRepository CreateFlightRepositoryWithTestData()
    {
        var flights = _fixture.CreateMany<FlightDomainModel>(5).ToList();
        var csvFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _csvFilePath);

        using (var writer = new StreamWriter(csvFilePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(flights.Select(f => new Flight { Id = f.Id }));
        }

        var mapper = new FlightMapper();
        var logger = NullLogger<FlightRepository>.Instance;
        return new FlightRepository(csvFilePath, mapper, logger);
    }
}
