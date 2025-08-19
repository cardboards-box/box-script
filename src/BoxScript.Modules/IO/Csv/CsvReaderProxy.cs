using CsvHelper;

namespace BoxScript.Modules.IO.Csv;

using Core;

/// <summary>
/// Represents a CSV reader that can read and parse CSV files
/// </summary>
public class CsvReaderProxy(StreamReader _reader) : IDisposable
{
    internal CsvReader? _csvReader;

    internal CsvReader Reader => _csvReader ??= new CsvReader(_reader, CultureInfo.InvariantCulture, true);

    /// <summary>
    /// Reads the current record from the CSV file
    /// </summary>
    [ModuleExport(type: typeof(JsValue))]
    public dynamic Current => Reader.GetRecord<dynamic>();

    /// <summary>
    /// Represents a CSV reader that can read and parse CSV files
    /// </summary>
    /// <param name="proxy">The reader proxy to use</param>
    public CsvReaderProxy(StreamReaderProxy proxy) : this(proxy.Reader) { }

    /// <summary>
    /// Reads all of the records in the CSV file 
    /// </summary>
    /// <returns>The unbuffered collection of records</returns>
    [ModuleExport(type: typeof(IteratorProxy))]
    public ScriptProxy RecordsUnbuffered()
    {
        return new(new IteratorProxy(Reader.GetRecords<dynamic>()));
    }

    /// <summary>
    /// Reads all of the records in the CSV file asynchronously
    /// </summary>
    /// <returns>The unbuffered collection of records</returns>
    [ModuleExport(type: typeof(IteratorProxyAsync))]
    public ScriptProxy RecordsUnbufferedAsync()
    {
        return new(new IteratorProxyAsync(Reader.GetRecordsAsync<dynamic>()));
    }

    /// <summary>
    /// Reads all of the records in the CSV file and returns them as an array
    /// </summary>
    /// <returns>All of the records from the file</returns>
    /// <remarks>This will load all records into memory - avoid for large files</remarks>
    [ModuleExport(type: typeof(JsValue[]))]
    public dynamic[] Records() => Reader.GetRecords<dynamic>().ToArray();

    /// <summary>
    /// Reads all of the records in the CSV file asynchronously and returns them as an array
    /// </summary>
    /// <returns></returns>
    /// <remarks>All of the records from the fileThis will load all records into memory - avoid for large files</remarks>
    [ModuleExport(type: typeof(ValueTask<JsValue[]>))]
    public ValueTask<dynamic[]> RecordsAsync() => Reader.GetRecordsAsync<dynamic>().ToArrayAsync();

    /// <summary>
    /// Reads the next record from the CSV file
    /// </summary>
    /// <returns>Whether or not the record was read</returns>
    [ModuleExport]
    public bool Read()
    {
        return Reader.Read();
    }

    /// <summary>
    /// Reads the next record from the CSV file
    /// </summary>
    /// <returns>Whether or not the record was read</returns>
    [ModuleExport]
    public Task<bool> ReadAsync() => Reader.ReadAsync();

    /// <summary>
    /// Reads the header of the CSV file
    /// </summary>
    /// <returns>Whether or not the headers were read</returns>
    [ModuleExport]
    public bool ReadHeader()
    {
        if (!Reader.Read()) return false;
        return Reader.ReadHeader();
    }

    /// <summary>
    /// Reads the header of the CSV file
    /// </summary>
    /// <returns>Whether or not the headers were read</returns>
    [ModuleExport]
    public async Task<bool> ReadHeaderAsync()
    {
        if (!await Reader.ReadAsync()) return false;
        return Reader.ReadHeader();
    }

    /// <summary>
    /// Reads the next record from the file
    /// </summary>
    /// <returns>The next record or null if there are none</returns>
    [ModuleExport(type: typeof(JsValue))]
    public dynamic? ReadNext()
    {
        if (!Read()) return null;
        return Current;
    }

    /// <summary>
    /// Reads the next record from the file
    /// </summary>
    /// <returns>The next record or null if there are none</returns>
    [ModuleExport(type: typeof(Task<JsValue>))]
    public async Task<dynamic?> ReadNextAsync()
    {
        if (!await ReadAsync()) return null;
        return Current;
    }

    /// <summary>
    /// Reads the row as an array of strings
    /// </summary>
    /// <returns>The array of strings or null if there are no records</returns>
    [ModuleExport(type: typeof(string[]))]
    public string?[]? ReadRow()
    {
        if (!Read()) return null;

        return Enumerable.Repeat(string.Empty, Reader.ColumnCount)
            .Select((_, i) => Reader.GetField(i))
            .ToArray();
    }

    /// <summary>
    /// Reads the row as an array of strings
    /// </summary>
    /// <returns>The array of strings or null if there are no records</returns>
    [ModuleExport(type: typeof(Task<string[]>))]
    public async Task<string?[]?> ReadRowAsync()
    {
        if (!await ReadAsync()) return null;

        return Enumerable.Repeat(string.Empty, Reader.ColumnCount)
            .Select((_, i) => Reader.GetField(i))
            .ToArray();
    }

    /// <summary>
    /// Disposes of the underlying CSV reader and stream reader.
    /// </summary>
    [ModuleExport]
    public void Dispose()
    {
        try
        {
            _csvReader?.Dispose();
            _reader.Dispose();
        }
        catch { }
        GC.SuppressFinalize(this);
    }
}
