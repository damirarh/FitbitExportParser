namespace FitbitExportParser.Cli.Csv;

/// <summary>
/// Service for writing to CSV files.
/// </summary>
public interface ICsvService
{
    /// <summary>
    /// Writes the specified data to the specified file path in CSV format.
    /// </summary>
    /// <remarks>
    /// If the file already exists, it will be overwritten.
    /// </remarks>
    /// <typeparam name="T">Type of data to write.</typeparam>
    /// <param name="filePath">Name of the file to write to.</param>
    /// <param name="data">Data to write to the file.</param>
    Task WriteAsync<T>(string filePath, IEnumerable<T> data);
}
