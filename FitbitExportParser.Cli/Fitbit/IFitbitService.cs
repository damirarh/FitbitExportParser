using FitbitExportParser.Cli.Fitbit.Models;

namespace FitbitExportParser.Cli.Fitbit;

/// <summary>
/// Service for loading Fitbit data.
/// </summary>
public interface IFitbitService
{
    /// <summary>
    /// Loads weight data from the specified root folder.
    /// </summary>
    /// <param name="rootFolder">Folder containing the Personal &amp; Account subfolder to read weight data from.</param>
    /// <returns>Weight data from all the weight-*.json files.</returns>
    IAsyncEnumerable<WeightEntry> LoadWeightDataAsync(string rootFolder);

    /// <summary>
    /// Loads distance data from the specified root folder.
    /// </summary>
    /// <param name="rootFolder">Folder containing the Physical Activity subfolder to read distance data from.</param>
    /// <returns>Distance data from all distance-*.json files.</returns>
    IAsyncEnumerable<DistanceEntry> LoadDistanceDataAsync(string rootFolder);

    /// <summary>
    /// Loads steps data from the specified root folder.
    /// </summary>
    /// <param name="rootFolder">Folder containing the Physical Activity subfolder to read steps data from.</param>
    /// <returns>Steps data from all steps-*.json files.</returns>
    IAsyncEnumerable<StepsEntry> LoadStepsDataAsync(string rootFolder);
}
