namespace BoxScript.Modules.IO;

using Core;

/// <summary>
/// A proxy for StreamWriter
/// </summary>
public class StreamWriterProxy(
    StreamWriter _writer)
{
    /// <summary>
    /// Provides access to the underlying StreamWriter instance
    /// </summary>
    internal StreamWriter Writer => _writer;

    /// <summary>
    /// Writes the given value to the stream writer
    /// </summary>
    /// <param name="value">The value to write</param>
    [ModuleExport]
    public void Write(string value) => _writer.Write(value);

    /// <summary>
    /// Writes the given value to the stream writer asynchronously
    /// </summary>
    /// <param name="value">The value to write</param>
    [ModuleExport]
    public Task WriteAsync(string value) => _writer.WriteAsync(value);

    /// <summary>
    /// Writes a line with the given value to the stream writer
    /// </summary>
    /// <param name="value">The value to write</param>
    [ModuleExport]
    public void WriteLine(string value) => _writer.WriteLine(value);

    /// <summary>
    /// Writes a line with the given value to the stream writer asynchronously
    /// </summary>
    /// <param name="value">The value to write</param>
    [ModuleExport]
    public Task WriteLineAsync(string value) => _writer.WriteLineAsync(value);

    /// <summary>
    /// Flushes the stream writer, ensuring all data is written to the underlying stream
    /// </summary>
    [ModuleExport]
    public void Flush() => _writer.Flush();

    /// <summary>
    /// Flushes the stream writer asynchronously, ensuring all data is written to the underlying stream
    /// </summary>
    [ModuleExport]
    public Task FlushAsync() => _writer.FlushAsync();

    /// <summary>
    /// Closes the stream writer, releasing any resources associated with it
    /// </summary>
    [ModuleExport]
    public void Close() => _writer.Close();

    /// <summary>
    /// Disposes of the stream writer, releasing any resources associated with it
    /// </summary>
    [ModuleExport]
    public void Dispose() => _writer.Dispose();
}
