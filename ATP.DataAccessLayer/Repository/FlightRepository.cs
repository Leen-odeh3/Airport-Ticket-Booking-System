using System.Globalization;
using ATP.BusinessLogicLayer.Models;
using ATP.DataAccessLayer.Mapper;
using ATP.DataAccessLayer.Models;
using CsvHelper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ATP.DataAccessLayer.Repository;

public class FlightRepository
{
    private readonly List<FlightDomainModel> _flights;
    private readonly string _csvFilePath;
    private readonly FlightMapper _mapper;
    private readonly ILogger<FlightRepository> _logger;

    public FlightRepository(string csvFilePath, FlightMapper mapper = null, ILogger<FlightRepository> logger = null)
    {
        _csvFilePath = csvFilePath;
        _mapper = mapper ?? new FlightMapper(); // Use a new FlightMapper if not provided
        _logger = logger ?? NullLogger<FlightRepository>.Instance; // Use null logger if not provided
        _flights = LoadFlightsFromCsv(csvFilePath);
    }

    private List<FlightDomainModel> LoadFlightsFromCsv(string csvFilePath)
    {
        try
        {
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var flights = csv.GetRecords<Flight>().ToList();
                var result = flights.Select(f => _mapper.MapToDomain(f)).ToList();
                return result;
            }
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "CSV file not found: {FilePath}", csvFilePath);
            return new List<FlightDomainModel>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load flights from CSV file: {FilePath}", csvFilePath);
            return new List<FlightDomainModel>();
        }
    }

    public FlightDomainModel GetById(int id)
    {
        return _flights.SingleOrDefault(f => f.Id == id);
    }

    public void Add(FlightDomainModel entity)
    {
        entity.Id = _flights.Count > 0 ? _flights.Max(f => f.Id) + 1 : 1;
        _flights.Add(entity);
        SaveChangesToCsv();
    }

    public void Delete(FlightDomainModel entity)
    {
        _flights.Remove(entity);
        SaveChangesToCsv();
    }

    public ICollection<FlightDomainModel> GetAll()
    {
        return _flights;
    }

    public void Update(FlightDomainModel entity)
    {
        var index = _flights.FindIndex(f => f.Id == entity.Id);
        if (index == -1) return;

        _flights[index] = entity;
        SaveChangesToCsv();
    }

    private void SaveChangesToCsv()
    {
        using var writer = new StreamWriter(_csvFilePath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(_flights);
    }
}
