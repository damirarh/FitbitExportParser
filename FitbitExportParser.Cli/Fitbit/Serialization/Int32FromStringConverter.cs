using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FitbitExportParser.Cli.Fitbit.Serialization;

/// <summary>
/// Deserializes a <see cref="int"/> from a string.
/// </summary>
/// <remarks>
/// Supports only deserialization; serialization is not implemented.
/// </remarks>
public class Int32FromStringConverter : JsonConverter<int>
{
    public override int Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var value = reader.GetString();
        return int.Parse(value ?? string.Empty, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
