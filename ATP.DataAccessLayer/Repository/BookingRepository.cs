using ATP.BusinessLogicLayer.Models;
using CsvHelper;
using System.Globalization;


namespace ATP.DataAccessLayer.Repository;

public class BookingRepository
{
    private readonly string _csvFilePath;

    public BookingRepository(string csvFilePath)
    {
        _csvFilePath = csvFilePath;
    }

    public void WriteBookingsToCsv(List<BookingDomainModel> bookings)
    {
        try
        {
            using (var writer = new StreamWriter(_csvFilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(bookings);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing bookings to CSV file: {ex.Message}");
        }
    }

    public List<BookingDomainModel> ReadBookingsFromCsv()
    {
        try
        {
            using (var reader = new StreamReader(_csvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<BookingDomainModel>().ToList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading bookings from CSV file: {ex.Message}");
            return new List<BookingDomainModel>();
        }
    }
}
