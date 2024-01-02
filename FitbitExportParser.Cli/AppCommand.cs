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

    [Option(
        Description = "Threshold for dividing by 10 the weight entries that are above the specified value. The resulting value will still be converted from pounds to kilograms if applicable."
    )]
    public double? WeightDivideBy10Threshold { get; set; }

    [Option(
        Description = "Threshold for converting weight from pounds to kilograms. If the weight is above this threshold, it will be converted to kilograms."
    )]
    public double? PoundConversionThreshold { get; set; }

    public async Task<int> OnExecuteAsync()
    {
        var historicalData = new HistoricalData();

        await historicalData.AddWeightEntriesAsync(
            fitbitService.LoadWeightDataAsync(Input),
            PoundConversionThreshold,
            WeightDivideBy10Threshold
        );
        await historicalData.AddDistanceEntriesAsync(fitbitService.LoadDistanceDataAsync(Input));
        await historicalData.AddStepsEntriesAsync(fitbitService.LoadStepsDataAsync(Input));
        await historicalData.AddSleepEntriesAsync(fitbitService.LoadSleepDataAsync(Input));

        await csvService.WriteAsync(Output, historicalData.DayEntries);

        return 0;
    }
}
