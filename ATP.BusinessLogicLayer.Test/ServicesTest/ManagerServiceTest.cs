using ATP.BusinessLogicLayer.Models;
using ATP.BusinessLogicLayer.Services;

namespace ATP.BusinessLogicLayer.Test.ServicesTest;
public class ManagerServiceTest
{  
    private static readonly List<FlightDomainModel> Flights = new List<FlightDomainModel>
    {
        new FlightDomainModel(1, 100.0, "USA", "UK", new DateTime(2024, 03, 10), "JFK", "LHR", FlightClass.Economy),
        new FlightDomainModel(2, 500.0, "UK", "France", new DateTime(2024, 03, 15), "LHR", "CDG", FlightClass.Business),
        new FlightDomainModel(3, 300.0, "Germany", "Italy", new DateTime(2024, 03, 20), "TXL", "FCO", FlightClass.First),
        new FlightDomainModel(4, 200.0, "Spain", "Greece", new DateTime(2024, 03, 25), "MAD", "ATH", FlightClass.Economy),
        new FlightDomainModel(5, 400.0, "Australia", "Japan", new DateTime(2024, 03, 30), "SYD", "HND", FlightClass.Business),
        new FlightDomainModel(6, 600.0, "China", "Singapore", new DateTime(2024, 04, 05), "PEK", "SIN", FlightClass.First)
    };

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
        var managerService = new ManagerService(Flights);

        SetupConsoleInput("USA\nUK\n");
        managerService.FilterByFlight();

        var expectedOutput = $"Filter by Flight:{Environment.NewLine}" +
                             $"Enter Departure Country: Enter Destination Country: Filtered Flights:{Environment.NewLine}" +
                             $"Flight ID: 1, Departure Country: USA, Destination Country: UK, Date: 3/10/2024 12:00:00 AM, Class: Economy, Price: 100{Environment.NewLine}";

        Assert.Equal(expectedOutput, _consoleOutput.ToString());
    }

    [Fact]
    public void FilterByClass_ReturnsCorrectResults_WhenGivenValidInput()
    {
        var managerService = new ManagerService(Flights);

        SetupConsoleInput("0\n");
        managerService.FilterByClass();

        var expectedOutput = $"Filter by Class:{Environment.NewLine}" +
                             $"Enter Class ( 0 for Economy, 1 for Business, 2 for FirstClass):{Environment.NewLine}Filtered Flights:{Environment.NewLine}" +
                             $"Flight ID: 1, Departure Country: USA, Destination Country: UK, Date: 3/10/2024 12:00:00 AM, Class: Economy, Price: 100{Environment.NewLine}" +
                             $"Flight ID: 4, Departure Country: Spain, Destination Country: Greece, Date: 3/25/2024 12:00:00 AM, Class: Economy, Price: 200{Environment.NewLine}";

        Assert.Equal(expectedOutput, _consoleOutput.ToString());
    }

    [Fact]
    public void FilterByFlight_ShouldReturnEmpty_WhenDestinationCountryNotFound()
    {
        var managerService = new ManagerService(Flights);

        SetupConsoleInput("USA\nNonExistentCountry\n");
        managerService.FilterByFlight();

        var expectedOutput = $"Filter by Flight:{Environment.NewLine}" +
                             $"Enter Departure Country: Enter Destination Country: No flights found matching the criteria.{Environment.NewLine}";

        Assert.Equal(expectedOutput, _consoleOutput.ToString());
    }

    [Fact]
    public void FilterByFlight_ShouldReturnEmpty_WhenDepartureCountryNotFound()
    {
        var managerService = new ManagerService(Flights);
        SetupConsoleInput("NonExistentCountry\nUK\n");
        managerService.FilterByFlight();

        var expectedOutput = $"Filter by Flight:{Environment.NewLine}" +
                             $"Enter Departure Country: Enter Destination Country: No flights found matching the criteria.{Environment.NewLine}";

        Assert.Equal(expectedOutput, _consoleOutput.ToString());
    }

    [Fact]
    public void FilterByFlight_ShouldReturnFlight_WhenDepartureAndDestinationMatch()
    {
        var managerService = new ManagerService(Flights);

        SetupConsoleInput("Germany\nItaly\n");
        managerService.FilterByFlight();

        var expectedOutput = $"Filter by Flight:{Environment.NewLine}" +
                             $"Enter Departure Country: Enter Destination Country: Filtered Flights:{Environment.NewLine}" +
                             $"Flight ID: 3, Departure Country: Germany, Destination Country: Italy, Date: 3/20/2024 12:00:00 AM, Class: First, Price: 300{Environment.NewLine}";

        Assert.Equal(expectedOutput, _consoleOutput.ToString());
    }
}
