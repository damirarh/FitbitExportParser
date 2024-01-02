# FitbitExportParser

The application parses a subset of data that can be exported from Fitbit [here](https://www.fitbit.com/settings/data/export):

- **Weight** from `Personal & Account/weight-*.json` files (use the latest data if there's more than one for a day, supports optional thresholds for converting the values from pounds to kilograms and for dividing the values by 10).
- **Distance** from `Physical Activity/distance-*.json` files (converts values from centimeters to kilometers, sums all data for a day).
- **Steps** from `Physical Activity/steps-*.json` files (sums all data for a day).
- **Time in bed** and **Time asleep** from `Sleep/sleep-*.json` files (uses only data from the main sleep of the day).

It saves the resulting per-day data in a CSV file that can easily be opened in a spreadsheet application.

See application help for available command line arguments:

```bash
FitbitExportParser --help
```
