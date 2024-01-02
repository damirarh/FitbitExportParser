namespace FitbitExportParser.Cli.Csv;

/// <summary>
/// Represents a single day entry in the aggregated CSV file.
/// </summary>
/// <param name="date">The date of the day entry.</param>
public class DayEntry(DateOnly date)
{
    /// <summary>
    /// Gets or sets the date of the day entry.
    /// </summary>
    public DateOnly Date { get; set; } = date;

    /// <summary>
    /// Gets or sets the weight in kilograms of the day entry.
    /// </summary>
    public double? Weight { get; set; }

    /// <summary>
    /// Gets or sets the distance in kilometers of the day entry.
    /// </summary>
    public double Distance { get; set; }

    /// <summary>
    /// Gets or sets the steps of the day entry.
    /// </summary>
    public double Steps { get; set; }
}
