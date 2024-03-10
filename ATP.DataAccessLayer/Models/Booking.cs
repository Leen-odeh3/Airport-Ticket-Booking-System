namespace ATP.DataAccessLayer.Models;
public class Booking
{
    public int BookingId { get; set; }
    public int PassengerId { get; set; }
    public int FlightId { get; set; }
    public string FlightClass { get; set; }
    public DateTime BookingDate { get; set; }
    public Passenger Passenger { get; set; }
    public Flight Flight { get; set; }
}
