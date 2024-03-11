using System;
using System.Collections.Generic;
using ATP.DataAccessLayer.Enum;
using ATP.DataAccessLayer.Models;

namespace ATP.BusinessLogicLayer.Services
{
    public class ManagerService
    {
        private readonly List<Flight> availableFlights;
        public ManagerService(List<Flight> availableFlights, BookingService bookingService)
        {
            this.availableFlights = availableFlights;
        }

        public void RunMenu()
        {
            while (true)
            {
                Console.WriteLine("Manager Menu");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("1. Filter by Flight");
                Console.WriteLine("2. Filter by Price");
                Console.WriteLine("3. Filter by Class");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        FilterByFlight();
                        break;
                    case "2":
                        FilterByPrice();
                        break;
                    case "3":
                        FilterByClass();
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

        private void FilterByFlight()
        {
            Console.WriteLine("Filter by Flight:");
            Console.Write("Enter Departure Country: ");
            string departureCountry = Console.ReadLine();

            Console.Write("Enter Destination Country: ");
            string destinationCountry = Console.ReadLine();

            var filteredFlights = availableFlights.FindAll(flight =>
                flight.DepartureCountry.Equals(departureCountry, StringComparison.OrdinalIgnoreCase) &&
                flight.DestinationCountry.Equals(destinationCountry, StringComparison.OrdinalIgnoreCase)
            );

            DisplayFilteredFlights(filteredFlights);
        }

        private void FilterByPrice()
        {
            Console.WriteLine("Filter by Price:");
            Console.Write("Enter Minimum Price: ");
            if (!double.TryParse(Console.ReadLine(), out double minPrice))
            {
                Console.WriteLine("Invalid input for minimum price.");
                return;
            }

            Console.Write("Enter Maximum Price: ");
            if (!double.TryParse(Console.ReadLine(), out double maxPrice))
            {
                Console.WriteLine("Invalid input for maximum price.");
                return;
            }

            var filteredFlights = availableFlights.FindAll(flight =>
                flight.Price >= minPrice && flight.Price <= maxPrice
            );

            DisplayFilteredFlights(filteredFlights);
        }
        private void FilterByClass()
        {
            Console.WriteLine("Filter by Class:");
            Console.WriteLine("Enter Class (Economy, Business, FirstClass):");
            string inputClass = Console.ReadLine().Trim();

            Enum.TryParse(inputClass, true, out FlightClass flightClass);

            var filteredFlights = availableFlights.FindAll(flight =>
                flight.Class == flightClass
            );

            DisplayFilteredFlights(filteredFlights);
        }

        private void DisplayFilteredFlights(List<Flight> flights)
        {
            if (flights.Count > 0)
            {
                Console.WriteLine("Filtered Flights:");
                foreach (var flight in flights)
                {
                    Console.WriteLine($"Flight ID: {flight.Id}, Departure Country: {flight.DepartureCountry}, Destination Country: {flight.DestinationCountry}, Date: {flight.DepartureDate}, Class: {flight.Class}, Price: {flight.Price}");
                }
            }
            else
            {
                Console.WriteLine("No flights found matching the criteria.");
            }
        }
    }
}
