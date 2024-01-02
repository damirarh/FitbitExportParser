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
            Converters = { new DateOnlyConverter(), new DateTimeConverter() },
        };

    public IAsyncEnumerable<WeightEntry> LoadWeightDataAsync(string rootFolder)
    {
        return LoadDataAsync<WeightEntry>(rootFolder, $"Personal & Account/weight-*.json");
    }

    public IAsyncEnumerable<DistanceEntry> LoadDistanceDataAsync(string rootFolder)
    {
        return LoadDataAsync<DistanceEntry>(rootFolder, $"Physical Activity/distance-*.json");
    }

    public IAsyncEnumerable<StepsEntry> LoadStepsDataAsync(string rootFolder)
    {
        return LoadDataAsync<StepsEntry>(rootFolder, $"Physical Activity/steps-*.json");
    }

    private static async IAsyncEnumerable<T> LoadDataAsync<T>(string rootFolder, string filePattern)
    {
        var matcher = new Matcher();
        matcher.AddInclude(filePattern);
        var matchingFiles = matcher.GetResultsInFullPath(rootFolder);
        foreach (var file in matchingFiles)
        {
            using var jsonStream = File.OpenRead(file);
            var jsonItems = JsonSerializer.DeserializeAsyncEnumerable<T>(
                jsonStream,
                JsonSerializerOptions
            );
            await foreach (var jsonItem in jsonItems)
            {
                if (jsonItem != null)
                {
                    yield return jsonItem;
                }
            }
        }
    }
}
