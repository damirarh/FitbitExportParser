using System.Text.Json.Serialization;
using FitbitExportParser.Cli.Fitbit.Serialization;

namespace FitbitExportParser.Cli.Fitbit.Models;

/// <summary>
/// Represents a single distance entry from exported Fitbit data.
/// </summary>
public class DistanceEntry
{
    /// <summary>
    /// Gets or sets the date and time of the distance entry.
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Gets or sets the value of the distance entry (in centimeters).
    /// </summary>
    [JsonConverter(typeof(DoubleFromStringConverter))]
    public double Value { get; set; }
}
