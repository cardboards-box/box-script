namespace BoxScript.Modules.IO;

using Core;

/// <summary>
/// A proxy for StreamReader
/// </summary>
public class StreamReaderProxy(
    StreamReader _reader)
{
    /// <summary>
    /// Reads a line from the stream reader
    /// </summary>
    /// <returns>The read line</returns>
    [ModuleExport]
    public string? ReadLine() => _reader.ReadLine();

    /// <summary>
    /// Reads a line from the stream reader asynchronously
    /// </summary>
    /// <returns>The read line</returns>
    [ModuleExport]
    public Task<string?> ReadLineAsync() => _reader.ReadLineAsync();

    /// <summary>
    /// Reads all remaining content from the stream reader
    /// </summary>
    /// <returns>The read content</returns>
    [ModuleExport]
    public string ReadToEnd() => _reader.ReadToEnd();

    /// <summary>
    /// Reads all remaining content from the stream reader asynchronously
    /// </summary>
    /// <returns>The read content</returns>
    [ModuleExport]
    public Task<string> ReadToEndAsync() => _reader.ReadToEndAsync();

    /// <summary>
    /// Closes the stream reader, releasing any resources associated with it
    /// </summary>
    [ModuleExport]
    public void Close() => _reader.Close();

    /// <summary>
    /// Disposes of the stream reader, releasing any resources associated with it
    /// </summary>
    [ModuleExport]
    public void Dispose() => _reader.Dispose();
}
