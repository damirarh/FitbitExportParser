namespace FitbitExportParser.Cli.Fitbit.Models;

/// <summary>
/// Represents a single weight entry from exported Fitbit data.
/// </summary>
public class WeightEntry
{
    /// <summary>
    /// Gets or sets the date of the weight entry.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the weight of the weight entry.
    /// </summary>
    public double Weight { get; set; }
}
