using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FitbitExportParser.Cli.Fitbit.Serialization;

/// <summary>
/// Deserializes a <see cref="DateTime"/> from a string in short US format: MM/dd/yy HH:mm:ss as used in exported Fitbit data.
/// </summary>
/// <remarks>
/// Supports only deserialization; serialization is not implemented.
/// </remarks>
public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var dateTime = reader.GetString();
        return DateTime.Parse(dateTime ?? string.Empty, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
