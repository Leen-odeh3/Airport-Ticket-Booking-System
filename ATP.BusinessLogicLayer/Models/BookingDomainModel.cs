namespace ATP.BusinessLogicLayer.Models
{
    public class BookingDomainModel
    {
        private int id;
        private FlightClass @class;
        private DateTime now;

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

        public BookingDomainModel(int bookingId, int id, FlightClass @class, DateTime now)
        {
            BookingId = bookingId;
            this.id = id;
            this.@class = @class;
            this.now = now;
        }
    }
}

public record BookingDto(string name);
