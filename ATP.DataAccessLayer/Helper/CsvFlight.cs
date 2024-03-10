
using ATP.DataAccessLayer.Models;
using CsvHelper;
using System.Formats.Asn1;
using System.Globalization;

namespace ATP.DataAccessLayer.Helper;
public class CsvFlight
{
    public void WriteFlightsToCsv(IEnumerable<Flight> flights, string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(flights);
        }
    }
}
