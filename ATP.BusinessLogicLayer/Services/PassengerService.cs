﻿using System;
using System.Collections.Generic;
using System.Linq;
using ATP.DataAccessLayer.Models;

namespace ATP.BusinessLogicLayer.Services
{
    public class PassengerService
    {
        private readonly List<Flight> availableFlights;
        private readonly BookingService bookingService;

        public PassengerService(List<Flight> availableFlights, BookingService bookingService)
        {
            this.availableFlights = availableFlights;
            this.bookingService = bookingService;
        }

        public void RunMenu()
        {
            Console.WriteLine("Passenger Menu");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1. Book a Flight");
            Console.WriteLine("2. Search for Available Flights");
            Console.WriteLine("3. View Personal Bookings");
            Console.WriteLine("4. Cancel Booking");
            Console.WriteLine("5. Go Back");
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
                    // SearchForAvailableFlights();
                    break;
                case 3:
                   ViewPersonalBookings();
                    break;
                case 4:
                    CancelBooking();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void BookFlight()
        {
            Console.WriteLine("Booking a flight...");

            Console.WriteLine("Available Flights:");
            foreach (var flight in availableFlights)
            {
                Console.WriteLine($"Flight ID: {flight.Id}, Departure: {flight.DepartureCountry}, Destination: {flight.DestinationCountry}, Date: {flight.DepartureDate}, Class: {flight.Class}");
            }

            Console.Write("Enter the ID of the flight you want to book: ");
            if (!int.TryParse(Console.ReadLine(), out int flightId))
            {
                Console.WriteLine("Invalid flight ID.");
                return;
            }

            var selectedFlight = availableFlights.FirstOrDefault(f => f.Id == flightId);
            if (selectedFlight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            bookingService.BookFlight(selectedFlight); // Use BookingService to book the flight
        }


        private void CancelBooking()
        {
            Console.Write("Enter booking ID to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out int bookingId))
            {
                Console.WriteLine("Invalid booking ID.");
                return;
            }

            bookingService.CancelBooking(bookingId);
        }


        private void ViewPersonalBookings()
        {
            Console.Write("Enter booking ID to view details: ");
            if (!int.TryParse(Console.ReadLine(), out int bookingId))
            {
                Console.WriteLine("Invalid booking ID.");
                return;
            }

            bookingService.ViewPersonalBookingDetails(bookingId);
        }

    }
}
