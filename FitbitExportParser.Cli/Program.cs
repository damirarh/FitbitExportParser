using FitbitExportParser.Cli;
using FitbitExportParser.Cli.Csv;
using FitbitExportParser.Cli.Fitbit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder()
    .ConfigureServices(ConfigureServices)
    .RunCommandLineApplicationAsync<AppCommand>(args);

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    services.AddTransient<IFitbitService, FitbitService>();
    services.AddTransient<ICsvService, CsvService>();
}
