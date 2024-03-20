using ATP.BusinessLogicLayer.Models;
using ATP.DataAccessLayer.Models;
using Riok.Mapperly.Abstractions;

namespace ATP.DataAccessLayer.Mapper;

[Mapper]

public partial class FlightMapper
{
    public FlightDomainModel MapToDomain(Flight model)
    {
        return new FlightDomainModel(
            model.Id,
            model.Price,
            model.DepartureCountry,
            model.DestinationCountry,
            model.DepartureDate,
            model.DepartureAirport,
            model.ArrivalAirport,
            (FlightClass)model.Class
        );
    }
}