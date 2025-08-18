namespace BoxScript.Modules.IO;

using Core;

/// <summary>
/// A module that provides directory functionality to box-scripts
/// </summary>
[Module("dir")]
public class DirectoryModule : IScriptModule
{
    /// <summary>
    /// Creates the given directory
    /// </summary>
    /// <param name="path">The directory to create</param>
    [ModuleExport]
    public void Create(string path) => Directory.CreateDirectory(path);

    /// <summary>
    /// Whether or not the given directory exists
    /// </summary>
    /// <param name="path">The directory to check</param>
    /// <returns>Whether or not the directory exists</returns>
    [ModuleExport]
    public bool Exists(string path) => Directory.Exists(path);

    /// <summary>
    /// Deletes the given directory
    /// </summary>
    /// <param name="path">The directory to delete</param>
    /// <param name="recursive">Whether or not to delete sub-directories</param>
    [ModuleExport]
    public void Delete(string path, bool recursive = true) => Directory.Delete(path, recursive);

    /// <summary>
    /// Gets the current directory
    /// </summary>
    /// <returns>The current directory</returns>
    [ModuleExport]
    public string Current() => Directory.GetCurrentDirectory();

    /// <summary>
    /// Moves the given directory to the destination
    /// </summary>
    /// <param name="source">The source path</param>
    /// <param name="destination">The destination path</param>
    [ModuleExport]
    public void Move(string source, string destination) => Directory.Move(source, destination);

    /// <summary>
    /// Gets the directories in the given path
    /// </summary>
    /// <param name="path">The path to get the files</param>
    /// <param name="searchPattern">The optional search pattern</param>
    /// <param name="options">The search options</param>
    /// <returns>The file paths</returns>
    [ModuleExport]
    public string[] Files(string path, string? searchPattern = null, SearchOption options = SearchOption.TopDirectoryOnly)
    {
        if (string.IsNullOrEmpty(searchPattern))
            return Directory.GetFiles(path);

        return Directory.GetFiles(path, searchPattern, options);
    }

    /// <summary>
    /// Gets the root directory or volume for the given path
    /// </summary>
    /// <param name="path">The directory path</param>
    /// <returns>The root directory or volume</returns>
    [ModuleExport]
    public string Root(string path) => Directory.GetDirectoryRoot(path);

    /// <summary>
    /// Gets the parent directory of the given path, or null if there is no parent
    /// </summary>
    /// <param name="path">The directory path</param>
    /// <returns>The parent directory</returns>
    [ModuleExport]
    public string? Parent(string path) => Directory.GetParent(path)?.FullName;
}
