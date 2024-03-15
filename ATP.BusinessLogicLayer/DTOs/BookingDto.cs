namespace ATP.BusinessLogicLayer.DTOs
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int FlightId { get; set; }
        public string FlightClass { get; set; }
        public DateTime BookingDate { get; set; }

        public BookingDTO(int bookingId, int flightId, string flightClass, DateTime bookingDate)
        {
            BookingId = bookingId;
            FlightId = flightId;
            FlightClass = flightClass;
            BookingDate = bookingDate;
        }
    }
}
