using ATP.BusinessLogicLayer.DTOs;
using CsvHelper;
using System.Globalization;

namespace ATP.BusinessLogicLayer.GenericRepo;
public class FlightRepository : IGenericRepo<FlightDto>
{ 
    private List<FlightDto> _flights;
    private readonly string _csvFilePath;

    public FlightRepository(string csvFilePath)
    {
        _csvFilePath = csvFilePath;
        _flights = LoadFlightsFromCsv(csvFilePath);
    }
    private List<FlightDto> LoadFlightsFromCsv(string csvFilePath)
    {
        List<FlightDto> flights = new List<FlightDto>();

        using var reader = new StreamReader(csvFilePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        flights = csv.GetRecords<FlightDto>().ToList();

        return flights;
    }
    public FlightDto GetById(int id)
    {
        return _flights.SingleOrDefault(f => f.Id == id);
    }


    public void Add(FlightDto entity)
    {
        entity.Id = _flights.Count > 0 ? _flights.Max(f => f.Id) + 1 : 1;
        _flights.Add(entity);
        SaveChangesToCsv();
    }

    public void Delete(FlightDto entity)
    {
        _flights.Remove(entity);
        SaveChangesToCsv();
    }

    public ICollection<FlightDto> GetAll()
    {
        return _flights;
    }

    public void Update(FlightDto entity)
    {
        var existingFlight = _flights.FirstOrDefault(f => f.Id == entity.Id);
        if (existingFlight is not null)
        {
            existingFlight = entity; 
            SaveChangesToCsv();
        }
    }


    private void SaveChangesToCsv()
    {
        using var writer = new StreamWriter(_csvFilePath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(_flights);
    }

}
