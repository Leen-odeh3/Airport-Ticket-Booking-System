using ATP.BusinessLogicLayer.Models;
using ATP.DataAccessLayer.Models;
using Riok.Mapperly.Abstractions;

namespace ATP.DataAccessLayer.Mapper;

[Mapper]
public partial class BookingMapper
{
    public BookingDomainModel MapToDomain(Booking model)
    {
        return new BookingDomainModel(
            model.BookingId,
            model.FlightId,
             (FlightClass)model.FlightClass,
            model.BookingDate,
            model.Flight.DepartureCountry,
            model.Flight.DestinationCountry
        );
    }
}