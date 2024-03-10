using ATP.DataAccessLayer.Enum;
using ATP.DataAccessLayer.Helper;
using ATP.DataAccessLayer.Models;

namespace ATP.PresentationLayer;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome !");

        var flights = new List<Flight>
        {
            new Flight(100.0, "USA", "UK", new DateTime(2024, 3, 10), "JFK", "LHR", FlightClass.Economy),
            new Flight(500.0, "UK", "France", new DateTime(2024, 3, 15), "LHR", "CDG", FlightClass.Business)
        };

        var csvFlightWriter = new CsvFlight();
        csvFlightWriter.WriteFlightsToCsv(flights, "C:\\Users\\hp\\Desktop\\C#\\AirportTicketBooking\\ATP.DataAccessLayer\\CsvFiles\\flights.csv");

        Console.WriteLine("Flights data has been written to flights.csv");
    }
}
