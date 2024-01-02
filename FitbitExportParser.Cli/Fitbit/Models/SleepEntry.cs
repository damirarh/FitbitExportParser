namespace FitbitExportParser.Cli.Fitbit.Models;

/// <summary>
/// Represents a single sleep entry from exported Fitbit data.
/// </summary>
public class SleepEntry
{
    /// <summary>
    /// Gets or sets the date of the sleep entry.
    /// </summary>
    public DateOnly DateOfSleep { get; set; }

    /// <summary>
    /// Gets or sets the time in bed of the sleep entry (in minutes).
    /// </summary>
    public int TimeInBed { get; set; }

    /// <summary>
    /// Gets or sets the minutes asleep of the sleep entry.
    /// </summary>
    public int MinutesAsleep { get; set; }

    /// <summary>
    /// Gets or sets whether the sleep entry is for the main sleep of the day.
    /// </summary>
    public bool MainSleep { get; set; }
}
