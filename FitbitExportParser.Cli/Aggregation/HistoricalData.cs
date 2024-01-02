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
    /// <remarks>
    /// Performs data cleanup and conversion from pounds to kilograms if necessary.
    /// </remarks>
    /// <param name="weightEntries">Weight data from the Fitbit export.</param>
    /// <param name="poundConversionThreshold">Converting values above threshold from pounds to kilograms.</param>
    /// <param name="divideBy10Threshold">Dividing values above threshold by 10. The result will sill be converted from pounds to kilograms if applicable.</param></param>
    public async Task AddWeightEntriesAsync(
        IAsyncEnumerable<WeightEntry> weightEntries,
        double? poundConversionThreshold,
        double? divideBy10Threshold
    )
    {
        await foreach (var weightEntry in weightEntries)
        {
            if (divideBy10Threshold.HasValue && weightEntry.Weight > divideBy10Threshold.Value)
            {
                weightEntry.Weight /= 10;
            }

            if (
                poundConversionThreshold.HasValue
                && weightEntry.Weight > poundConversionThreshold.Value
            )
            {
                weightEntry.Weight /= 2.2046213;
            }

            dayEntries[weightEntry.Date] = new DayEntry(weightEntry.Date)
            {
                Weight = Math.Round(weightEntry.Weight, 1, MidpointRounding.AwayFromZero)
            };
        }
    }
}
