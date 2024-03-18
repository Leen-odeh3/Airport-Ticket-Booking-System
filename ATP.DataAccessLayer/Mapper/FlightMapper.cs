using ATP.BusinessLogicLayer.Models;
using ATP.DataAccessLayer.Models;
using Riok.Mapperly.Abstractions;

namespace ATP.DataAccessLayer.Mapper;

[Mapper]
public partial class FlightMapper
{
    public partial FlightDomainModel MapToDomain(Flight model);
}