namespace ATP.BusinessLogicLayer.Models
{
    public class BookingDomainModel
    {
        public int BookingId { get; set; }
        public int FlightId { get; set; }
        public FlightClass FlightClass { get; set; }
        public DateTime BookingDate { get; set; }

        public BookingDomainModel(int bookingId, int flightId, FlightClass flightClass, DateTime bookingDate)
        {
            BookingId = bookingId;
            FlightId = flightId;
            FlightClass = flightClass;
            BookingDate = bookingDate;
        }
    }
}

public record BookingDto(string name);
