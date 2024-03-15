
namespace ATP.BusinessLogicLayer.DTOs;

public class FlightDto
{
    public int Id { get; set; }
    public double Price { get; set; }
    public string DepartureCountry { get; set; }
    public string DestinationCountry { get; set; }
    public DateTime DepartureDate { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }
    public FlightClassDTO Class { get; set; }
}
