
using ATP.DataAccessLayer.Enum;

namespace ATP.BusinessLogicLayer.Services;

public class PassengerService
{
    public void RunMenu()
    {
        Console.WriteLine("Passenger Menu");
        Console.WriteLine("--------------------------------------");
        Console.WriteLine("1. Book a Flight");
        Console.WriteLine("2. Search for Available Flights");
        Console.WriteLine("3. Manage Bookings");
        Console.WriteLine("4. Go Back");
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
                SearchForAvailableFlights();
                break;
            case 3:
                ManageBookings();
                break;
            case 4:
                // Go back to the previous menu
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    private void BookFlight()
    {
        Console.WriteLine("Booking a flight...");

        Console.Write("Enter the Flight ID: ");
        int flightId;
        while (!int.TryParse(Console.ReadLine(), out flightId))
        {
            Console.WriteLine("Invalid input. Please enter a valid Flight ID: ");
        }

        Console.Write("Enter the Flight Class (Economy, Business, FirstClass): ");
        string classInput = Console.ReadLine();
        FlightClass flightClass;
        while (!Enum.TryParse(classInput, out flightClass))
        {
            Console.WriteLine("Invalid input. Please enter a valid Flight Class: ");
            classInput = Console.ReadLine();
        }

        Console.WriteLine("Flight successfully booked! \n");
    }


    private void SearchForAvailableFlights()
    {
        Console.WriteLine("Searching for available flights...");
    }

    private void ManageBookings()
    {
        Console.WriteLine("Manage Bookings");
        Console.WriteLine("----------------------");
        Console.WriteLine("1. Cancel a Booking");
        Console.WriteLine("2. Modify a Booking");
        Console.WriteLine("3. View Personal Bookings");
        Console.WriteLine("4. Go Back");
        Console.Write("Enter your choice: ");

        int manageChoice;
        if (!int.TryParse(Console.ReadLine(), out manageChoice))
        {
            Console.WriteLine("Invalid choice. Please try again.");
            return;
        }

        switch (manageChoice)
        {
            case 1:
                CancelBooking();
                break;
            case 2:
                ModifyBooking();
                break;
            case 3:
                ViewPersonalBookings();
                break;
       
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    private void CancelBooking()
    {
        Console.WriteLine("Canceling a booking...");
    }

    private void ModifyBooking()
    {
        Console.WriteLine("Modifying a booking...");
    }

    private void ViewPersonalBookings()
    {
        Console.WriteLine("Viewing personal bookings...");
    }
}
