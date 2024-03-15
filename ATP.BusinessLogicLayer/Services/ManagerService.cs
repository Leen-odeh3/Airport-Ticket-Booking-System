using System;
using System.Collections.Generic;
using ATP.BusinessLogicLayer.DTOs;

namespace ATP.BusinessLogicLayer.Services
{
    public class ManagerService
    {
        private readonly List<FlightDto> availableFlights;

        public ManagerService(List<FlightDto> flights)
        {
            availableFlights = flights;
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

            if (minPrice > maxPrice)
            {
                Console.WriteLine("Minimum price cannot be greater than maximum price.");
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

            if (!Enum.TryParse(inputClass, true, out FlightClassDTO flightClass))
            {
                Console.WriteLine("Invalid class input.");
                return;
            }

            var filteredFlights = availableFlights.FindAll(flight =>
                flight.Class == flightClass
            );

            DisplayFilteredFlights(filteredFlights);
        }


        private void DisplayFilteredFlights(List<FlightDto> flights)
        {
            if (flights.Count > 0)
            {
                Console.WriteLine("Filtered Flights:");
                foreach (var flight in flights)
                {
                    Console.WriteLine($"Flight ID: {flight.Id}, Departure Country: {flight.DepartureCountry}, Destination Country: {flight.DestinationCountry}, Date: {flight.DepartureDate}, Class: {flight.Class.ClassName}, Price: {flight.Price}");
                }
            }
            else
            {
                Console.WriteLine("No flights found matching the criteria.");
            }
        }
    }
}
