using System.Linq;

namespace ATP.BusinessLogicLayer.Services
{
    using ATP.DataAccessLayer.Models;
    public class BookingService
    {
        private List<Booking> bookings;

        public BookingService()
        {
            bookings = new List<Booking>();
        }

        public void CancelBooking(int bookingId)
        {
            var booking = bookings.Find(b => b.BookingId== bookingId);
            if (booking is not null)
            {
                bookings.Remove(booking);
                Console.WriteLine("Booking canceled successfully.");
            }
            else
            {
                Console.WriteLine("Booking not found.");
            }
        }

        public void ModifyBooking(int bookingId, Flight newFlight)
        {
            var booking = bookings.Find(b => b.BookingId == bookingId);
            if (booking is not null)
            {
                booking.Flight = newFlight;
                Console.WriteLine("Booking modified successfully.");
            }
            else
            {
                Console.WriteLine("Booking not found.");
            }
        }

        public List<Booking> ViewPersonalBookings(string passengerName)
        {
            var personalBookings = bookings.Where(b => b.Passenger.Name == passengerName).ToList();
            if (personalBookings.Count == 0)
            {
                Console.WriteLine("No bookings found for this passenger.");
            }
            return personalBookings;
        }
    }
}
