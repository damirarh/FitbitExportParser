using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FitbitExportParser.Cli.Fitbit.Serialization;

/// <summary>
/// Deserializes a <see cref="DateOnly"/> from a string in short US format: MM/dd/yy as used in exported Fitbit data.
/// </summary>
/// <remarks>
/// Supports only deserialization; serialization is not implemented.
/// </remarks>
public class DateOnlyConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var date = reader.GetString();
        return DateOnly.Parse(date ?? string.Empty, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
