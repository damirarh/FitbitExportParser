using System.Text.Json;
using FitbitExportParser.Cli.Fitbit;
using FitbitExportParser.Cli.Fitbit.Models;
using FluentAssertions;

namespace FitbitExportParser.Tests;

public class JsonSerializationTests
{
    [Test]
    public void WeightDeserializesCorrectly()
    {
        var json = """
            {
                "logId" : 1627862399000,
                "weight" : 125.8,
                "bmi" : 24.71,
                "date" : "08/01/21",
                "time" : "23:59:59",
                "source" : "API"
            }
            """;

        var weightEntry = JsonSerializer.Deserialize<WeightEntry>(
            json,
            FitbitService.JsonSerializerOptions
        );

        weightEntry.Should().NotBeNull();
        weightEntry!.Weight.Should().Be(125.8);
        weightEntry!.Date.Should().Be(new DateOnly(2021, 8, 1));
    }
}
