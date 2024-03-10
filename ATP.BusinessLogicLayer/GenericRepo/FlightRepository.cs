using ATP.DataAccessLayer.IGenericRepo;
using ATP.DataAccessLayer.Models;
using CsvHelper;
using System.Globalization;

namespace ATP.BusinessLogicLayer.GenericRepo;
public class FlightRepository : IGenericRepo<Flight>
{ 
    private List<Flight> _flights;

    public FlightRepository(string csvFilePath)
    {
        _flights = LoadFlightsFromCsv(csvFilePath);
    }
    private List<Flight> LoadFlightsFromCsv(string csvFilePath)
    {
        List<Flight> flights = new List<Flight>();

        using (var reader = new StreamReader(csvFilePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            flights = csv.GetRecords<Flight>().ToList();
        }

        return flights;
    }
    public Flight GetById(int id)
    {
        return _flights.FirstOrDefault(f => f.Id == id);
    }

    public void Add(Flight entity)
    {
        entity.Id = _flights.Count > 0 ? _flights.Max(f => f.Id) + 1 : 1;
        _flights.Add(entity);
    }

    public void Delete(Flight entity)
    {
        _flights.Remove(entity);
    }

    public IEnumerable<Flight> GetAll()
    {
        return _flights;
    }

    public void Update(Flight entity)
    {
        var existingFlight = _flights.FirstOrDefault(f => f.Id == entity.Id);
        if (existingFlight is not null)
        {
            existingFlight.Price = entity.Price;
            existingFlight.DepartureCountry = entity.DepartureCountry;
            existingFlight.DestinationCountry = entity.DestinationCountry;
            existingFlight.DepartureDate = entity.DepartureDate;
            existingFlight.DepartureAirport = entity.DepartureAirport;
            existingFlight.ArrivalAirport = entity.ArrivalAirport;
            existingFlight.Class = entity.Class;
        }
    }
}
