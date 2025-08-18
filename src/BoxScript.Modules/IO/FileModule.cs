namespace BoxScript.Modules.IO;

using Core;
using System.IO.Compression;

/// <summary>
/// A module that provides file system related functionalities to box-scripts
/// </summary>
[Module("file")]
public class FileModule : IScriptModule
{
    /// <summary>
    /// Copies the given file to the destination
    /// </summary>
    /// <param name="source">The source file</param>
    /// <param name="destination">The destination path</param>
    /// <param name="overwrite">Whether or not to overwrite the file</param>
    [ModuleExport]
    public void Copy(string source, string destination, bool overwrite = true) => File.Copy(source, destination, overwrite);

    /// <summary>
    /// Moves the given file to the destination
    /// </summary>
    /// <param name="source">The source file to move</param>
    /// <param name="destination">The destination path</param>
    /// <param name="overwrite">Whether or not to overwrite the file</param>
    [ModuleExport]
    public void Move(string source, string destination, bool overwrite = true) => File.Move(source, destination, overwrite);

    /// <summary>
    /// Checks if the given file exists
    /// </summary>
    /// <param name="path">The file path</param>
    /// <returns>Whether or not the file exists</returns>
    [ModuleExport]
    public bool Exists(string path) => File.Exists(path);

    /// <summary>
    /// Deletes the given file
    /// </summary>
    /// <param name="path">The file path</param>
    [ModuleExport]
    public void Delete(string path) => File.Delete(path);

    /// <summary>
    /// Appends the given text to the end of the file
    /// </summary>
    /// <param name="path">The file path</param>
    /// <param name="content">The content</param>
    [ModuleExport]
    public void Append(string path, string content) => File.AppendAllText(path, content);

    /// <summary>
    /// Writes the given content to the file, overwriting any existing content
    /// </summary>
    /// <param name="path">The file path</param>
    /// <param name="content">The content</param>
    [ModuleExport]
    public void Write(string path, string content) => File.WriteAllText(path, content);

    /// <summary>
    /// Gets the creation time of the given file
    /// </summary>
    /// <param name="path">The file path</param>
    /// <returns>The creation time of the given file</returns>
    [ModuleExport]
    public DateTime CreateTime(string path) => File.GetCreationTime(path);

    /// <summary>
    /// Gets the last time the file was accessed
    /// </summary>
    /// <param name="path">The file path</param>
    /// <returns>The last time the file was accessed</returns>
    [ModuleExport]
    public DateTime AccessTime(string path) => File.GetLastAccessTime(path);

    /// <summary>
    /// Gets the last time the file was written to
    /// </summary>
    /// <param name="path">The file path</param>
    /// <returns>The last time the file was written to</returns>
    [ModuleExport]
    public DateTime WriteTime(string path) => File.GetLastWriteTime(path);

    /// <summary>
    /// Opens a file for reading as a text stream
    /// </summary>
    /// <param name="path">The file path</param>
    /// <returns>The file stream</returns>
    [ModuleExport(type: typeof(StreamReaderProxy))]
    public ScriptProxy OpenRead(string path) => new(new StreamReaderProxy(File.OpenText(path)));

    /// <summary>
    /// Opens a file for writing as a text stream
    /// </summary>
    /// <param name="path">The file path</param>
    /// <returns>The file stream</returns>
    [ModuleExport(type: typeof(StreamWriterProxy))]
    public ScriptProxy OpenWrite(string path) => new(new StreamWriterProxy(new StreamWriter(File.OpenWrite(path))));

    /// <summary>
    /// Creates a new file at the specified path
    /// </summary>
    /// <param name="path">The file path</param>
    /// <returns>The file stream</returns>
    [ModuleExport(type: typeof(StreamWriterProxy))]
    public ScriptProxy Create(string path) => new(new StreamWriterProxy(File.CreateText(path)));

    /// <summary>
    /// Ensures a file is created at the given path
    /// </summary>
    /// <param name="path">The file path</param>
    [ModuleExport]
    public void Touch(string path)
    {
        if (File.Exists(path)) return;
        File.WriteAllText(path, string.Empty);
    }

    /// <summary>
    /// Extracts a zip file to the specified folder
    /// </summary>
    /// <param name="path">The zip file to extract</param>
    /// <param name="folder">The folder to extract to</param>
    /// <param name="overwrite">Whether or not to overwrite files</param>
    [ModuleExport]
    public void Unzip(string path, string folder, bool overwrite = true)
    {
        ZipFile.ExtractToDirectory(path, folder, overwrite);
    }

    /// <summary>
    /// Compresses a directory into a zip file
    /// </summary>
    /// <param name="path">The folder to compress</param>
    /// <param name="destination">The destination zip file</param>
    /// <param name="includeBaseDir">Whether or not to include the base directory in the file</param>
    /// <param name="level">The compression level</param>
    [ModuleExport]
    public void Zip(string path, string destination, bool includeBaseDir = true, CompressionLevel level = CompressionLevel.Optimal)
    {
        ZipFile.CreateFromDirectory(path, destination, level, includeBaseDir);
    }
}
