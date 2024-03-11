using ATP.DataAccessLayer.Enum;
using ATP.DataAccessLayer.Models;
using System;

namespace ATP.BusinessLogicLayer.Services
{
    public class PassengerService
    {
        private readonly BookingService _bookingService;

        public PassengerService(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public PassengerService()
        {
            _bookingService = new BookingService(); 
        }

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

            Booking newBooking = new Booking()
            {
                FlightId = flightId,
                FlightClass = flightClass.ToString(),
            };

            _bookingService.AddBooking(newBooking);

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
                    ViewBookingDetails();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void CancelBooking()
        {
            Console.WriteLine("Canceling a booking...");
            Console.Write("Enter the Booking ID to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out int bookingId))
            {
                Console.WriteLine("Invalid input. Please enter a valid Booking ID.");
                return;
            }

            _bookingService.CancelBooking(bookingId);
        }

        private void ModifyBooking()
        {
            Console.WriteLine("Modifying a booking...");
            Console.Write("Enter the Booking ID to modify: ");
            if (!int.TryParse(Console.ReadLine(), out int bookingId))
            {
                Console.WriteLine("Invalid input. Please enter a valid Booking ID.");
                return;
            }

            _bookingService.CancelBooking(bookingId);
            BookFlight();
        }

        public void ViewBookingDetails()
        {
            Console.WriteLine("Viewing booking details...");
            Console.Write("Enter the Booking ID: ");
            int bookingId;
            if (!int.TryParse(Console.ReadLine(), out bookingId))
            {
                Console.WriteLine("Invalid input. Please enter a valid Booking ID.");
                return;
            }

            Booking booking = _bookingService.GetBookingById(bookingId);
            if (booking is not null)
            {
                Console.WriteLine($"Booking ID: {booking.BookingId}");
                Console.WriteLine($"Passenger ID: {booking.PassengerId}");
                Console.WriteLine($"Flight ID: {booking.FlightId}");
                Console.WriteLine($"Flight Class: {booking.FlightClass}");
                Console.WriteLine($"Booking Date: {booking.BookingDate}");
            }
            else
            {
                Console.WriteLine($"Booking with ID {bookingId} does not exist. OR You dont book any Flight");
            }
        }
    }
}
