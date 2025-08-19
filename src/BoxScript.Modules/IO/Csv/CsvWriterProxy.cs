using CsvHelper;

namespace BoxScript.Modules.IO.Csv;

using Core;

/// <summary>
/// Represents a CSV writer
/// </summary>
public class CsvWriterProxy(StreamWriter _writer) : IDisposable
{
    internal CsvWriter? _csvWriter;

    internal CsvWriter Writer => _csvWriter ??= new CsvWriter(_writer, CultureInfo.InvariantCulture, true);

    /// <summary>
    /// Represents a CSV writer
    /// </summary>
    /// <param name="proxy">The writer proxy to use</param>
    public CsvWriterProxy(StreamWriterProxy proxy) : this(proxy.Writer) { }

    /// <summary>
    /// Writes all of the given records to the CSV file
    /// </summary>
    /// <param name="value">The values to write</param>
    /// <exception cref="ArgumentException">Thrown if the input isn't an array</exception>
    [ModuleExport]
    public void WriteRecords([ModuleExport(type: typeof(JsValue[]))] JsValue value)
    {
        if (!value.IsArray())
            throw new ArgumentException("value needs to be a collection of values");

        var items = value.AsArray().Select(t => t.ToObject());
        Writer.WriteRecords(items);
        Writer.Flush();
    }

    /// <summary>
    /// Disposes of the underlying CSV writer and stream writer.
    /// </summary>
    [ModuleExport]
    public void Dispose()
    {
        try
        {
            _csvWriter?.Flush();
            _csvWriter?.Dispose();
            _writer.Dispose();
        }
        catch { }
        GC.SuppressFinalize(this);
    }
}
