using System.Globalization;
using System.Text;
using CsvHelper;

namespace FitbitExportParser.Cli.Csv;

/// <summary>
/// Service for writing to CSV files.
/// </summary>
public class CsvService : ICsvService
{
    public async Task WriteAsync<T>(string filePath, IEnumerable<T> data)
    {
        using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        await csv.WriteRecordsAsync(data);
    }
}
