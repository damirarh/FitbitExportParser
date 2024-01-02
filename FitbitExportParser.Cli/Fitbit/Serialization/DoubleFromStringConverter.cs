using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FitbitExportParser.Cli.Fitbit.Serialization;

/// <summary>
/// Deserializes a <see cref="double"/> from a string with comma as the decimal separator.
/// </summary>
/// <remarks>
/// Supports only deserialization; serialization is not implemented.
/// </remarks>
public class DoubleFromStringConverter : JsonConverter<double>
{
    public override double Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var value = reader.GetString();
        return double.Parse(value ?? string.Empty, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
