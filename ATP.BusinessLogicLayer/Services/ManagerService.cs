using ATP.BusinessLogicLayer.Models;

namespace ATP.BusinessLogicLayer.Services;

public class ManagerService
{
    private readonly List<FlightDomainModel> availableFlights;

    public ManagerService(List<FlightDomainModel> flights)
    {
        availableFlights = flights;
    }
    public List<FlightDomainModel> FilterByFlight(string departureCountry, string destinationCountry)
    {
        var filteredFlights = availableFlights.FindAll(flight =>
            flight.DepartureCountry.Equals(departureCountry, StringComparison.OrdinalIgnoreCase) &&
            flight.DestinationCountry.Equals(destinationCountry, StringComparison.OrdinalIgnoreCase)
        );

        return filteredFlights;
    }

    public List<FlightDomainModel> FilterByPrice(double minPrice, double maxPrice)
    {
        if (minPrice > maxPrice)
        {
            Console.WriteLine("Minimum price cannot be greater than maximum price.");
            return new List<FlightDomainModel>();
        }

        var filteredFlights = availableFlights.FindAll(flight =>
            flight.Price >= minPrice && flight.Price <= maxPrice
        );

        DisplayFilteredFlights(filteredFlights);
        return filteredFlights;
    }
    public List<FlightDomainModel> FilterByClass(FlightClass flightClass)
    {
        var filteredFlights = availableFlights.FindAll(flight =>
            flight.Class == flightClass
        );

        DisplayFilteredFlights(filteredFlights);
        return filteredFlights;
    }


    private void DisplayFilteredFlights(List<FlightDomainModel> flights)
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
