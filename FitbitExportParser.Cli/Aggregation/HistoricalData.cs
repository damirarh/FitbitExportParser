using FitbitExportParser.Cli.Csv;
using FitbitExportParser.Cli.Fitbit.Models;

namespace FitbitExportParser.Cli.Aggregation;

/// <summary>
/// Represents aggregated historical data.
/// </summary>
public class HistoricalData
{
    private readonly Dictionary<DateOnly, DayEntry> dayEntries = [];

    /// <summary>
    /// Gets the day entries sorted by date ascending.
    /// </summary>
    public IEnumerable<DayEntry> DayEntries => dayEntries.Values.OrderBy(dayEntry => dayEntry.Date);

    /// <summary>
    /// Adds the weight entries to the historical data.
    /// </summary>
    /// <param name="weightEntries">Weight data from the Fitbit export.</param>
    public async Task AddWeightEntriesAsync(IAsyncEnumerable<WeightEntry> weightEntries)
    {
        await foreach (var weightEntry in weightEntries)
        {
            dayEntries[weightEntry.Date] = new DayEntry(weightEntry.Date)
            {
                Weight = weightEntry.Weight
            };
        }
    }
}
