using System.Text.Json;
using FitbitExportParser.Cli.Fitbit.Models;
using FitbitExportParser.Cli.Fitbit.Serialization;
using Microsoft.Extensions.FileSystemGlobbing;

namespace FitbitExportParser.Cli.Fitbit;

public class FitbitService : IFitbitService
{
    /// <summary>
    /// Gets the <see cref="System.Text.Json.JsonSerializerOptions"/> used for deserializing Fitbit data.
    /// </summary>
    public static JsonSerializerOptions JsonSerializerOptions { get; } =
        new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new DateOnlyConverter() }
        };

    public async IAsyncEnumerable<WeightEntry> LoadWeightDataAsync(string rootFolder)
    {
        var matcher = new Matcher();
        matcher.AddInclude($"Personal & Account/weight-*.json");
        var matchingFiles = matcher.GetResultsInFullPath(rootFolder);
        foreach (var file in matchingFiles)
        {
            using var jsonStream = File.OpenRead(file);
            var weightEntries = JsonSerializer.DeserializeAsyncEnumerable<WeightEntry>(
                jsonStream,
                JsonSerializerOptions
            );
            await foreach (var weightEntry in weightEntries)
            {
                if (weightEntry != null)
                {
                    yield return weightEntry;
                }
            }
        }
    }
}
