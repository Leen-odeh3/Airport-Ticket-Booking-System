using ATP.BusinessLogicLayer.Models;
using ATP.BusinessLogicLayer.Services;

namespace ATP.BusinessLogicLayer.Test.ServicesTest;
public class ManagerServiceTest
{
    static string filePath = "C:\\Users\\hp\\Desktop\\C#\\ATP.BusinessLogicLayer.Test\\ServicesTest\\TestFlights.json";
    List<FlightDomainModel> flights = ManagerService.LoadFlightsFromJson(filePath);

    private StringWriter _consoleOutput;
    private StringReader _consoleInput;

    public ManagerServiceTest()
    {
        _consoleOutput = new StringWriter();
        Console.SetOut(_consoleOutput);
    }

    private void SetupConsoleInput(string input)
    {
        _consoleInput = new StringReader(input);
        Console.SetIn(_consoleInput);
    }

    [Fact]
    public void FilterByFlight_ShouldReturnCorrectResults_WhenGivenInput()
    {
        var managerService = new ManagerService(flights);

        SetupConsoleInput("USA\nUK\n");
        managerService.FilterByFlight();

        string actualOutput = _consoleOutput.ToString();
        string expectedOutput = $"Filter by Flight:{Environment.NewLine}" +
                                $"Enter Departure Country: Enter Destination Country: Filtered Flights:{Environment.NewLine}" +
                                $"Flight ID: 1, Departure Country: USA, Destination Country: UK, Date: 3/10/2024 12:00:00 AM, Class: Economy, Price: 100{Environment.NewLine}";

        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void FilterByFlight_ShouldReturnEmpty_WhenDestinationCountryNotFound()
    {
        var managerService = new ManagerService(flights);

        SetupConsoleInput("USA\nNonExistentCountry\n");
        managerService.FilterByFlight();

        var expectedOutput = $"Filter by Flight:{Environment.NewLine}" +
                            $"Enter Departure Country: Enter Destination Country: No flights found matching the criteria.{Environment.NewLine}";

        Assert.Equal(expectedOutput, _consoleOutput.ToString());
    }

    [Fact]
    public void FilterByFlight_ShouldReturnEmpty_WhenDepartureCountryNotFound()
    {
        var managerService = new ManagerService(flights);
        SetupConsoleInput("NonExistentCountry\nUK\n");
        managerService.FilterByFlight();

        var expectedOutput = $"Filter by Flight:{Environment.NewLine}" +
                            $"Enter Departure Country: Enter Destination Country: No flights found matching the criteria.{Environment.NewLine}";

        Assert.Equal(expectedOutput, _consoleOutput.ToString());
    }

    [Fact]
    public void FilterByFlight_ShouldReturnFlight_WhenDepartureAndDestinationMatch()
    {
        var managerService = new ManagerService(flights);

        SetupConsoleInput("Germany\nItaly\n");
        managerService.FilterByFlight();

        var expectedOutput = $"Filter by Flight:{Environment.NewLine}" +
                            $"Enter Departure Country: Enter Destination Country: Filtered Flights:{Environment.NewLine}" +
                            $"Flight ID: 3, Departure Country: Germany, Destination Country: Italy, Date: 3/20/2024 12:00:00 AM, Class: First, Price: 300{Environment.NewLine}";

        Assert.Equal(expectedOutput, _consoleOutput.ToString());
    }
}
