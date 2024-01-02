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
    public Task AddWeightEntriesAsync(
        IAsyncEnumerable<WeightEntry> weightEntries,
        double? poundConversionThreshold,
        double? divideBy10Threshold
    )
    {
        void weightMutator(DayEntry dayEntry, WeightEntry weightEntry)
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

            dayEntry.Weight = Math.Round(weightEntry.Weight, 1, MidpointRounding.AwayFromZero);
        }

        DateOnly dateSelector(WeightEntry weightEntry) => weightEntry.Date;

        return AddEntriesAsync(weightEntries, weightMutator, dateSelector);
    }

    /// <summary>
    /// Adds the distance entries to the historical data.
    /// </summary>
    /// <remarks>Converts distance from centimeters to kilometers and rounds values to 3 decimal digits.</remarks>
    /// <param name="distanceEntries">Distance data from the Fitbit export.</param>
    public Task AddDistanceEntriesAsync(IAsyncEnumerable<DistanceEntry> distanceEntries)
    {
        void distanceMutator(DayEntry dayEntry, DistanceEntry distanceEntry)
        {
            dayEntry.Distance = Math.Round(
                dayEntry.Distance + distanceEntry.Value / 100 / 1000,
                3,
                MidpointRounding.AwayFromZero
            );
        }

        DateOnly dateSelector(DistanceEntry distanceEntry) =>
            DateOnly.FromDateTime(distanceEntry.DateTime);

        return AddEntriesAsync(distanceEntries, distanceMutator, dateSelector);
    }

    /// <summary>
    /// Adds the steps entries to the historical data.
    /// </summary>
    /// <param name="stepsEntries">Steps data from the Fitbit export.</param>
    public Task AddStepsEntriesAsync(IAsyncEnumerable<StepsEntry> stepsEntries)
    {
        void stepsMutator(DayEntry dayEntry, StepsEntry stepsEntry)
        {
            dayEntry.Steps += stepsEntry.Value;
        }

        DateOnly dateSelector(StepsEntry stepsEntry) => DateOnly.FromDateTime(stepsEntry.DateTime);

        return AddEntriesAsync(stepsEntries, stepsMutator, dateSelector);
    }

    private async Task AddEntriesAsync<T>(
        IAsyncEnumerable<T> fitbitEntries,
        Action<DayEntry, T> dayEntryMutator,
        Func<T, DateOnly> dateSelector
    )
    {
        await foreach (var fitbitEntry in fitbitEntries)
        {
            var date = dateSelector(fitbitEntry);

            if (!dayEntries.TryGetValue(date, out var dayEntry))
            {
                dayEntry = new DayEntry(date);
                dayEntries[dayEntry.Date] = dayEntry;
            }

            dayEntryMutator(dayEntry, fitbitEntry);
        }
    }
}
