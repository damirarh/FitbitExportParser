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

    [Test]
    public void SleepDeserializesCorrectly()
    {
        var json = """
                        {
              "logId" : 33582911665,
              "dateOfSleep" : "2021-08-30",
              "startTime" : "2021-08-30T12:16:30.000",
              "endTime" : "2021-08-30T13:27:30.000",
              "duration" : 4260000,
              "minutesToFallAsleep" : 0,
              "minutesAsleep" : 66,
              "minutesAwake" : 5,
              "minutesAfterWakeup" : 0,
              "timeInBed" : 71,
              "efficiency" : 93,
              "type" : "classic",
              "infoCode" : 2,
              "logType" : "auto_detected",
              "levels" : {
                "summary" : {
                  "restless" : {
                    "count" : 3,
                    "minutes" : 5
                  },
                  "awake" : {
                    "count" : 0,
                    "minutes" : 0
                  },
                  "asleep" : {
                    "count" : 0,
                    "minutes" : 66
                  }
                },
                "data" : [{
                  "dateTime" : "2021-08-30T12:16:30.000",
                  "level" : "asleep",
                  "seconds" : 2280
                },{
                  "dateTime" : "2021-08-30T12:54:30.000",
                  "level" : "restless",
                  "seconds" : 120
                },{
                  "dateTime" : "2021-08-30T12:56:30.000",
                  "level" : "asleep",
                  "seconds" : 60
                },{
                  "dateTime" : "2021-08-30T12:57:30.000",
                  "level" : "restless",
                  "seconds" : 120
                },{
                  "dateTime" : "2021-08-30T12:59:30.000",
                  "level" : "asleep",
                  "seconds" : 1440
                },{
                  "dateTime" : "2021-08-30T13:23:30.000",
                  "level" : "restless",
                  "seconds" : 60
                },{
                  "dateTime" : "2021-08-30T13:24:30.000",
                  "level" : "asleep",
                  "seconds" : 180
                }]
              },
              "mainSleep" : false
            }
            """;

        var sleepEntry = JsonSerializer.Deserialize<SleepEntry>(
            json,
            FitbitService.JsonSerializerOptions
        );

        sleepEntry.Should().NotBeNull();
        sleepEntry!.DateOfSleep.Should().Be(new DateOnly(2021, 8, 30));
        sleepEntry!.MinutesAsleep.Should().Be(66);
        sleepEntry!.TimeInBed.Should().Be(71);
        sleepEntry!.MainSleep.Should().BeFalse();
    }
}
