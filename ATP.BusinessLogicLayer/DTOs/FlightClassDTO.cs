namespace ATP.BusinessLogicLayer.DTOs;
public class FlightClassDTO
{
    public string ClassName { get; set; }
    public FlightClassDTO(string className)
    {
        ClassName = className;
    }
}
