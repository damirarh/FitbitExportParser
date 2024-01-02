using System.ComponentModel.DataAnnotations;
using FitbitExportParser.Cli.Aggregation;
using FitbitExportParser.Cli.Csv;
using FitbitExportParser.Cli.Fitbit;
using McMaster.Extensions.CommandLineUtils;

namespace FitbitExportParser.Cli;

/// <summary>
/// Implements the root/only command of the application.
/// </summary>
public class AppCommand(IFitbitService fitbitService, ICsvService csvService)
{
    [Required]
    [Option(
        Description = "Root folder containing data subfolders, such as Physical Activity, Sleep, etc. Required."
    )]
    public string Input { get; set; } = null!;

    [Required]
    [Option(Description = "Name for the generated CSV file. Required.")]
    public string Output { get; set; } = null!;

    public async Task<int> OnExecuteAsync()
    {
        var historicalData = new HistoricalData();

        await historicalData.AddWeightEntriesAsync(fitbitService.LoadWeightDataAsync(Input));

        await csvService.WriteAsync(Output, historicalData.DayEntries);

        return 0;
    }
}
