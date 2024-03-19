
namespace ATP.BusinessLogicLayer.Models;
public class FlightDomainModel
{
    public FlightDomainModel(int id, double price, string departureCountry, string destinationCountry, DateTime departureDate, string departureAirport, string arrivalAirport, FlightClass flightClass)
    {
        Id = id;
        Price = price;
        DepartureCountry = departureCountry;
        DestinationCountry = destinationCountry;
        DepartureDate = departureDate;
        DepartureAirport = departureAirport;
        ArrivalAirport = arrivalAirport;
        Class = flightClass;
    }
    public int Id { get; set; }
    public double Price { get; set; }
    public string DepartureCountry { get; set; }
    public string DestinationCountry { get; set; }
    public DateTime DepartureDate { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }
    public FlightClass Class { get; set; }
}