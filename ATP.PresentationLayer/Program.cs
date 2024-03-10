using System.Globalization;
using ATP.DataAccessLayer.Enum;
using ATP.DataAccessLayer.Helper;
using ATP.DataAccessLayer.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace ATP.PresentationLayer;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome !");
        var flights = new List<Flight>
        {
            new Flight(1, 100.0, "USA", "UK", new DateTime(2024, 3, 10), "JFK", "LHR", FlightClass.Economy),
            new Flight(2, 500.0, "UK", "France", new DateTime(2024, 3, 15), "LHR", "CDG", FlightClass.Business),
            new Flight(3, 300.0, "Germany", "Italy", new DateTime(2024, 3, 20), "TXL", "FCO", FlightClass.FirstClass),
            new Flight(4, 200.0, "Spain", "Greece", new DateTime(2024, 3, 25), "MAD", "ATH", FlightClass.Economy),
            new Flight(5, 400.0, "Australia", "Japan", new DateTime(2024, 3, 30), "SYD", "HND", FlightClass.Business),
            new Flight(6, 600.0, "China", "Singapore", new DateTime(2024, 4, 5), "PEK", "SIN", FlightClass.FirstClass)
        };

        var csvFlightWriter = new CsvFlight();
        csvFlightWriter.WriteFlightsToCsv(flights, "C:\\Users\\hp\\Desktop\\C#\\AirportTicketBooking\\ATP.DataAccessLayer\\CsvFiles\\flights.csv");

        Console.WriteLine("Flights data has been written to flights.csv \n");
        ReadAndDisplayCsvData("C:\\Users\\hp\\Desktop\\C#\\AirportTicketBooking\\ATP.DataAccessLayer\\CsvFiles\\flights.csv");

        DisplayMainMenu();
    }

    static void DisplayMainMenu()
    {
        Console.WriteLine(@"Welcome to the Airport Ticket Booking System!
--------------------------------------------
1. Passenger
2. Manager
3. Exit");
        Console.Write("Enter your role (1 for Passenger, 2 for Manager, 3 to Exit): ");

        if (!int.TryParse(Console.ReadLine(), out int roleChoice))
        {
            Console.WriteLine("Invalid choice. Please enter a valid number.");
            return;
        }

        switch (roleChoice)
        {
            case 1:
                RunPassengerMenu();
                break;
            case 2:
                RunManagerMenu();
                break;
            case 3:
                Console.WriteLine("Exiting the application. Goodbye!");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

        static void RunPassengerMenu()
    {
        Console.WriteLine("Passenger Menu");
        Console.WriteLine("Passenger features will be available soon!");
        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
        DisplayMainMenu();
    }

    static void RunManagerMenu()
    {
        Console.WriteLine("Manager Menu");
        Console.WriteLine("Manager features will be available soon!");
        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
        DisplayMainMenu();
    }

    static void ReadAndDisplayCsvData(string csvFilePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
        };

        using (var reader = new StreamReader(csvFilePath))
        using (var csv = new CsvReader(reader, config))
        {
            try
            {
                var records = csv.GetRecords<dynamic>().ToList();
                Console.WriteLine("Data from CSV: (Available Flights) ");
                foreach (var record in records)
                {
                    foreach (var property in record)
                    {
                        Console.WriteLine($"{property.Key}: {property.Value}");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }
    }
}
