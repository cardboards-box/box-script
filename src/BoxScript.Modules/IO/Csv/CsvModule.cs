namespace BoxScript.Modules.IO.Csv;

using Core;

/// <summary>
/// A module that provides CSV functionality to box-scripts
/// </summary>
[Module("csv")]
public class CsvModule : IScriptModule
{
    /// <summary>
    /// Reads the given CSV file
    /// </summary>
    /// <param name="path">The file path</param>
    /// <returns>The CSV reader instance</returns>
    /// <remarks>Make sure to read the headers!</remarks>
    [ModuleExport(type: typeof(CsvReaderProxy))]
    public ScriptProxy Read(string path)
    {
        var sr = new StreamReader(path);
        return new(new CsvReaderProxy(sr));
    }

    /// <summary>
    /// Reads the given file stream
    /// </summary>
    /// <param name="reader">The stream reader</param>
    /// <returns>The CSV reader instance</returns>
    /// <remarks>Make sure to read the headers!</remarks>
    [ModuleExport(type: typeof(CsvReaderProxy))]
    public ScriptProxy ReadStream(StreamReaderProxy reader)
    {
        return new(new CsvReaderProxy(reader));
    }

    /// <summary>
    /// Writes the given CSV file
    /// </summary>
    /// <param name="path">The path to write to</param>
    /// <returns>The CSV writer instance</returns>
    /// <remarks>Make sure to write the headers</remarks>
    [ModuleExport(type: typeof(CsvWriterProxy))]
    public ScriptProxy Write(string path)
    {
        var sw = new StreamWriter(path);
        return new(new CsvWriterProxy(sw));
    }

    /// <summary>
    /// Writes the given file stream
    /// </summary>
    /// <param name="writer">The stream writer</param>
    /// <returns>The CSV writer instance</returns>
    /// <remarks>Make sure to write the headers!</remarks>
    [ModuleExport(type: typeof(CsvReaderProxy))]
    public ScriptProxy WriteStream(StreamWriterProxy writer)
    {
        return new(new CsvWriterProxy(writer));
    }
}
