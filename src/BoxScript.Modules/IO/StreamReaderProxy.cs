namespace BoxScript.Modules.IO;

using Core;

/// <summary>
/// A proxy for StreamReader
/// </summary>
public class StreamReaderProxy(
    StreamReader _reader)
{
    /// <summary>
    /// Provides access to the underlying StreamReader instance
    /// </summary>
    internal StreamReader Reader => _reader;

    /// <summary>
    /// A proxy for StreamReader
    /// </summary>
    /// <param name="stream">The stream to read</param>
    public StreamReaderProxy(Stream stream) : this(new StreamReader(stream)) { }

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

    /// <summary>
    /// Save the current stream to a file
    /// </summary>
    /// <param name="path">The file to save to</param>
    [ModuleExport]
    public async Task SaveToFile(string path)
    {
        using var io = File.Create(path);
        await Reader.BaseStream.CopyToAsync(io);
        await io.FlushAsync();
    }

    /// <summary>
    /// Copy the content of the stream to another stream
    /// </summary>
    /// <param name="stream">The stream to write to</param>
    [ModuleExport]
    public async Task CopyTo(StreamWriterProxy stream)
    {
        await Reader.BaseStream.CopyToAsync(stream.Writer.BaseStream);
        await stream.FlushAsync();
    }
}
