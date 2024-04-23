using ATP.BusinessLogicLayer.Models;
using ATP.BusinessLogicLayer.Abstract;

namespace ATP.BusinessLogicLayer.Services;

public class PassengerService : IPassengerService
{
    private int nextBookingId;
    private readonly List<FlightDomainModel> _availableFlights;
    private readonly BookingService _bookingService;

    public PassengerService(List<FlightDomainModel> availableFlights, BookingService bookingService)
    {
        _availableFlights = availableFlights;
        _bookingService = bookingService;
        nextBookingId = 1;
    }

    public void RunMenu()
    {
        Console.WriteLine(@"Passenger Menu
--------------------------------------
1. Book a Flight
2. View Personal Bookings
3. Cancel Booking
4. Go Back");
        Console.Write("Enter your choice: ");

        int passengerChoice;
        if (!int.TryParse(Console.ReadLine(), out passengerChoice))
        {
            Console.WriteLine("Invalid choice. Please try again.");
            return;
        }

        switch (passengerChoice)
        {
            case 1:
                BookFlight();
                break;
            case 2:
                ViewPersonalBookings();
                break;
            case 3:
                CancelBooking();
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    public void BookFlight()
    {
        Console.WriteLine("Booking a flight...");

        Console.WriteLine("Available Flights:");
        foreach (var flight in _availableFlights)
        {
            Console.WriteLine($"Flight ID: {flight.Id}, Departure: {flight.DepartureCountry}, Destination: {flight.DestinationCountry}, Date: {flight.DepartureDate}, Class: {flight.Class}");
        }

        Console.Write("Enter the ID of the flight you want to book: ");
        if (!int.TryParse(Console.ReadLine(), out int flightId))
        {
            Console.WriteLine("Invalid flight ID.");
            return;
        }

        var selectedFlight = _availableFlights.FirstOrDefault(f => f.Id == flightId);
        if (selectedFlight is null)
        {
            Console.WriteLine("Flight not found.");
            return;
        }

        _bookingService.BookFlight(selectedFlight);
    }


   public void ViewPersonalBookings()
    {
        Console.Write("Enter booking ID to view details: ");
        if (!int.TryParse(Console.ReadLine(), out int bookingId))
        {
            Console.WriteLine("Invalid booking ID.");
            return;
        }

        var booking = _bookingService.ViewPersonalBookingDetails(bookingId);
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

    private void CancelBooking()
    {
        Console.Write("Enter booking ID to cancel: ");
        if (!int.TryParse(Console.ReadLine(), out int bookingId))
        {
            Console.WriteLine("Invalid booking ID.");
            return;
        }

        _bookingService.CancelBooking(bookingId);
    }
}
