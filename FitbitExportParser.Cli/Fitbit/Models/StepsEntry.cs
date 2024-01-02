using System.Text.Json.Serialization;
using FitbitExportParser.Cli.Fitbit.Serialization;

namespace FitbitExportParser.Cli.Fitbit.Models;

/// <summary>
/// Represents a single steps entry from exported Fitbit data.
/// </summary>
public class StepsEntry
{
    /// <summary>
    /// Gets or sets the date and time of the steps entry.
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Gets or sets the value of the steps entry.
    /// </summary>
    [JsonConverter(typeof(Int32FromStringConverter))]
    public int Value { get; set; }
}
