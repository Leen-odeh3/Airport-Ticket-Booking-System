using ATP.BusinessLogicLayer.Models;
using ATP.BusinessLogicLayer.Services;
using ATP.DataAccessLayer.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using FlightClass = ATP.DataAccessLayer.Enum.FlightClass;

namespace ATP.PresentationLayer
{
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

            var flights = new List<FlightDomainModel>
            {
                new FlightDomainModel(1, 100.0, "USA", "UK", new DateTime(2024, 3, 10), "JFK", "LHR", (BusinessLogicLayer.Models.FlightClass)FlightClass.Economy),
                new FlightDomainModel(2, 500.0, "UK", "France", new DateTime(2024, 3, 15), "LHR", "CDG", (BusinessLogicLayer.Models.FlightClass)FlightClass.Business),
                new FlightDomainModel(3, 300.0, "Germany", "Italy", new DateTime(2024, 3, 20), "TXL", "FCO", (BusinessLogicLayer.Models.FlightClass)FlightClass.FirstClass),
                new FlightDomainModel(4, 200.0, "Spain", "Greece", new DateTime(2024, 3, 25), "MAD", "ATH", (BusinessLogicLayer.Models.FlightClass)FlightClass.Economy),
                new FlightDomainModel(5, 400.0, "Australia", "Japan", new DateTime(2024, 3, 30), "SYD", "HND", (BusinessLogicLayer.Models.FlightClass)FlightClass.Business),
                new FlightDomainModel(6, 600.0, "China", "Singapore", new DateTime(2024, 4, 5), "PEK", "SIN", (BusinessLogicLayer.Models.FlightClass)FlightClass.FirstClass)
            };

            var bookingService = new BookingService(NullLogger<BookingService>.Instance);

            Console.WriteLine("Welcome !");
            var csvFilePath = configuration["CsvFilePath"];
            FlightRepository flightRepository = new(csvFilePath);
            flightRepository.WriteListToCsv(flights);


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
                        ManagerService managerService = new ManagerService(flights);
                        RunMenu(managerService);
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
                    Console.WriteLine($"Departure: {b.DepartureCountry}");
                    Console.WriteLine($"Destination: {b.DestinationCountry}");
                    Console.WriteLine($"Date: {b.BookingDate}");
                    Console.WriteLine($"Class: {b.FlightClass}");
                }

                Console.WriteLine();
            } while (mainChoice != 3);
        }

        public void DisplayPersonalBookingDetails(int bookingId, BookingService bookingService)
        {
            var booking = bookingService.ViewPersonalBookingDetails(bookingId);
            if (booking is not null)
            {
                Console.WriteLine($"Booking ID: {booking.BookingId}");
                Console.WriteLine($"Flight Details:");
                Console.WriteLine($"Departure: {booking.DepartureCountry}");
                Console.WriteLine($"Destination: {booking.DestinationCountry}");
                Console.WriteLine($"Date: {booking.BookingDate}");
                Console.WriteLine($"Class: {booking.FlightClass}");

            }
            else
            {
                Console.WriteLine("Booking not found.");
            }
        }

        public static void RunMenu(ManagerService managerService)
        {
            while (true)
            {
                Console.WriteLine(@"Manager Menu
--------------------------------------
1. Filter by Flight
2. Filter by Price
3. Filter by Class
4. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        managerService.FilterByFlight();
                        break;
                    case "2":
                        managerService.FilterByPrice();
                        break;
                    case "3":
                        managerService.FilterByClass();
                        break;
                    case "4":
                        Console.WriteLine("Exiting Manager Menu.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
