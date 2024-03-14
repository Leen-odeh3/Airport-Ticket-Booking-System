﻿using ATP.DataAccessLayer.Models;
using Microsoft.Extensions.Logging;
public class BookingService
{
    private List<Booking> bookings;
    private int nextBookingId;
    private readonly ILogger<BookingService> _logger;
    public BookingService(ILogger<BookingService> logger)
    {
        bookings = new List<Booking>();
        nextBookingId = 1;
        _logger = logger;
    }
    public void BookFlight(Flight flight)
    {
        int bookingId = nextBookingId++;
        var booking = new Booking
        {
            BookingId = bookingId,
            Flight = flight
        };
        bookings.Add(booking);
        _logger.LogInformation($"Booking with ID {bookingId} successfully created for the flight from {flight.DepartureCountry} to {flight.DestinationCountry} on {flight.DepartureDate}.");


    }
    public void CancelBooking(int bookingId)
    {
        var booking = bookings.Find(b => b.BookingId == bookingId);
        if (booking is not null)
        {
            bookings.Remove(booking);
            _logger.LogInformation("Booking canceled successfully.");
        }
        else
        {
            _logger.LogInformation("Booking not found.");
        }
    }

    public List<Booking> GetBookings()
    {
        return bookings;
    }
    public Booking ViewPersonalBookingDetails(int bookingId)
    {
        return bookings.Find(b => b.BookingId == bookingId);
    }

    public List<Booking> FilterBookings(Func<Booking, bool> predicate)
    {
        return bookings.Where(predicate).ToList();
    }

}
