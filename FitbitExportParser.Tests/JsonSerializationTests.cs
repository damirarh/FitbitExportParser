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

    [Test]
    public void DistanceDeserializesCorrectly()
    {
        var json = """
            {
              "dateTime" : "12/31/12 18:58:00",
              "value" : "753.6"
            }
            """;

        var distanceEntry = JsonSerializer.Deserialize<DistanceEntry>(
            json,
            FitbitService.JsonSerializerOptions
        );

        distanceEntry.Should().NotBeNull();
        distanceEntry!.DateTime.Should().Be(new DateTime(2012, 12, 31, 18, 58, 0));
        distanceEntry!.Value.Should().Be(753.6);
    }

    [Test]
    public void StepsDeserializeCorrectly()
    {
        var json = """
            {
              "dateTime" : "06/12/16 03:51:00",
              "value" : "133"
            }
            """;

        var stepsEntry = JsonSerializer.Deserialize<StepsEntry>(
            json,
            FitbitService.JsonSerializerOptions
        );

        stepsEntry.Should().NotBeNull();
        stepsEntry!.DateTime.Should().Be(new DateTime(2016, 6, 12, 3, 51, 0));
        stepsEntry!.Value.Should().Be(133);
    }
}
