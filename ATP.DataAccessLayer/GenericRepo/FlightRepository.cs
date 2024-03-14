using ATP.BusinessLogicLayer.IGenericRepo;
using ATP.DataAccessLayer.Models;
using CsvHelper;
using System.Globalization;

namespace ATP.BusinessLogicLayer.GenericRepo;
public class FlightRepository : IGenericRepo<Flight>
{ 
    private List<Flight> _flights;
    private readonly string _csvFilePath;

    public FlightRepository(string csvFilePath)
    {
        _csvFilePath = csvFilePath;
        _flights = LoadFlightsFromCsv(csvFilePath);
    }
    private List<Flight> LoadFlightsFromCsv(string csvFilePath)
    {
        List<Flight> flights = new List<Flight>();

        using var reader = new StreamReader(csvFilePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        flights = csv.GetRecords<Flight>().ToList();

        return flights;
    }
    public Flight GetById(int id)
    {
        return _flights.SingleOrDefault(f => f.Id == id);
    }


    public void Add(Flight entity)
    {
        entity.Id = _flights.Count > 0 ? _flights.Max(f => f.Id) + 1 : 1;
        _flights.Add(entity);
        SaveChangesToCsv();
    }

    public void Delete(Flight entity)
    {
        _flights.Remove(entity);
        SaveChangesToCsv();
    }

    public ICollection<Flight> GetAll()
    {
        return _flights;
    }

    public void Update(Flight entity)
    {
        var existingFlight = _flights.FirstOrDefault(f => f.Id == entity.Id);
        if (existingFlight is not null)
        {
            var entityType = typeof(Flight);
            var properties = entityType.GetProperties();

            foreach (var property in properties)
            {
                var newValue = property.GetValue(entity);
                property.SetValue(existingFlight, newValue);
            }

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
