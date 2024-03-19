namespace ATP.BusinessLogicLayer.Models
{
    public class BookingDomainModel
    {
        public int BookingId { get; set; }
        public int FlightId { get; set; }
        public FlightClass FlightClass { get; set; }
        public DateTime BookingDate { get; set; }
        public string DepartureCountry { get; set; } 
        public string DestinationCountry { get; set; } 

        public BookingDomainModel(int bookingId, int flightId, FlightClass flightClass, DateTime bookingDate, string departureCountry, string destinationCountry)
        {
            BookingId = bookingId;
            FlightId = flightId;
            FlightClass = flightClass;
            BookingDate = bookingDate;
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
        }
    }
}

public record BookingDto(string name);
