using System;
using System.Collections.Generic;
using ATP.BusinessLogicLayer.Services;
using ATP.DataAccessLayer.Enum;
using ATP.DataAccessLayer.Helper;
using ATP.DataAccessLayer.Models;

namespace ATP.PresentationLayer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Airport Ticket Booking App!");
            Console.WriteLine("-------------------------------------");

            var flights = new List<Flight>
            {
                new Flight(1, 100.0, "USA", "UK", new DateTime(2024, 3, 10), "JFK", "LHR", FlightClass.Economy),
                new Flight(2, 500.0, "UK", "France", new DateTime(2024, 3, 15), "LHR", "CDG", FlightClass.Business),
                new Flight(3, 300.0, "Germany", "Italy", new DateTime(2024, 3, 20), "TXL", "FCO", FlightClass.FirstClass),
                new Flight(4, 200.0, "Spain", "Greece", new DateTime(2024, 3, 25), "MAD", "ATH", FlightClass.Economy),
                new Flight(5, 400.0, "Australia", "Japan", new DateTime(2024, 3, 30), "SYD", "HND", FlightClass.Business),
                new Flight(6, 600.0, "China", "Singapore", new DateTime(2024, 4, 5), "PEK", "SIN", FlightClass.FirstClass)
            };

            var bookingService = new BookingService();

            Console.WriteLine("Welcome !");
            var csvFlightWriter = new CsvFlight();
            csvFlightWriter.WriteFlightsToCsv(flights, "C:\\Users\\hp\\Desktop\\C#\\ATP.DataAccessLayer\\CsvFiles\\flights.csv");

            while (true)
            {
                Console.WriteLine("1. Passenger");
                Console.WriteLine("2. Manager");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");

                string mainChoice = Console.ReadLine();

                switch (mainChoice)
                {
                    case "1":
                        PassengerService passengerService = new PassengerService(flights, bookingService);
                        passengerService.RunMenu();
                        break;
                    case "2":
                       
                        break;
                    case "3":
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
