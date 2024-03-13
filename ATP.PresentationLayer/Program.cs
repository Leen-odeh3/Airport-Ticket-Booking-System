using ATP.BusinessLogicLayer.Services;
using ATP.DataAccessLayer.Enum;
using ATP.DataAccessLayer.Helper;
using ATP.DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;



namespace ATP.PresentationLayer;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Airport Ticket Booking App!");
        Console.WriteLine("-------------------------------------");

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var configuration = builder.Build();

        var flights = new List<Flight>
        {
            new Flight(1, 100.0, "USA", "UK", new DateTime(2024, 3, 10), "JFK", "LHR", FlightClass.Economy),
            new Flight(2, 500.0, "UK", "France", new DateTime(2024, 3, 15), "LHR", "CDG", FlightClass.Business),
            new Flight(3, 300.0, "Germany", "Italy", new DateTime(2024, 3, 20), "TXL", "FCO", FlightClass.FirstClass),
            new Flight(4, 200.0, "Spain", "Greece", new DateTime(2024, 3, 25), "MAD", "ATH", FlightClass.Economy),
            new Flight(5, 400.0, "Australia", "Japan", new DateTime(2024, 3, 30), "SYD", "HND", FlightClass.Business),
            new Flight(6, 600.0, "China", "Singapore", new DateTime(2024, 4, 5), "PEK", "SIN", FlightClass.FirstClass)
        };

        var bookingService = new BookingService(NullLogger<BookingService>.Instance);

        Console.WriteLine("Welcome !");
        var csvFilePath = configuration["CsvFilePath"];
        var csvFlightWriter = new CsvFlight();
        csvFlightWriter.WriteFlightsToCsv(flights, csvFilePath);

        int mainChoice;
        do
        {
            Console.WriteLine(@"1. Passenger
2. Manager
3. Exit");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out mainChoice))
            {
                Console.WriteLine("Invalid choice. Please enter a number.");
                continue;
            }

            switch (mainChoice)
            {
                case 1:
                    PassengerService passengerService = new PassengerService(flights, bookingService);
                    passengerService.RunMenu();
                    break;
                case 2:
                    ManagerService managerService = new ManagerService(flights, bookingService);
                    managerService.RunMenu();
                    break;
                case 3:
                    Console.WriteLine("Exiting the application. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine("Current Bookings:");
            foreach (var b in bookingService.GetBookings())
            {
                Console.WriteLine($"Booking ID: {b.BookingId}");
                Console.WriteLine($"Flight Details:");
                Console.WriteLine($"   Departure: {b.Flight.DepartureCountry}");
                Console.WriteLine($"   Destination: {b.Flight.DestinationCountry}");
                Console.WriteLine($"   Date: {b.Flight.DepartureDate}");
                Console.WriteLine($"   Class: {b.Flight.Class}");
            }
            Console.WriteLine();
        }
        while (mainChoice != 3);
    }
}
