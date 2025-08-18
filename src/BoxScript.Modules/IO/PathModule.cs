namespace BoxScript.Modules.IO;

using Core;

/// <summary>
/// A module that provides path related functionalities to box-scripts
/// </summary>
[Module("path")]
public class PathModule : IScriptModule
{
    /// <summary>
    /// The character used to separate path segments
    /// </summary>
    [ModuleExport]
    public string DirChar => Path.DirectorySeparatorChar.ToString();

    /// <summary>
    /// The alternate character used to separate path segments
    /// </summary>
    [ModuleExport]
    public string AltDirChar => Path.AltDirectorySeparatorChar.ToString();

    /// <summary>
    /// The character used to separate paths in environment variables
    /// </summary>
    [ModuleExport]
    public string Separator => Path.PathSeparator.ToString();

    /// <summary>
    /// The character used to separate the volume from the path
    /// </summary>
    [ModuleExport]
    public string VolumeSeparator => Path.VolumeSeparatorChar.ToString();

    /// <summary>
    /// Changes the extension of the given path
    /// </summary>
    /// <param name="path">The path to change</param>
    /// <param name="extension">The extension to use</param>
    /// <returns>The changed path</returns>
    [ModuleExport]
    public string ChangeExtension(string path, string? extension)
    {
        return Path.ChangeExtension(path, extension);
    }

    /// <summary>
    /// Combines the given paths into a single path
    /// </summary>
    /// <param name="paths">The path segments</param>
    /// <returns>The combined path</returns>
    [ModuleExport]
    public string Combine(params string[] paths) => Path.Combine(paths);

    /// <summary>
    /// Checks if the given path exists
    /// </summary>
    /// <param name="path">The path to check</param>
    /// <returns>Whether or not the path exists</returns>
    [ModuleExport]
    public bool Exists(string path) => Path.Exists(path);

    /// <summary>
    /// Whether or not the given path ends in the directory separator
    /// </summary>
    /// <param name="path">The path to check</param>
    /// <returns>Whether or not the given path ends in the directory separator</returns>
    [ModuleExport]
    public bool EndsInDirSeparator(string path) => Path.EndsInDirectorySeparator(path);

    /// <summary>
    /// Gets the directory name for the given path
    /// </summary>
    /// <param name="path">The path to check</param>
    /// <returns>The directory path</returns>
    [ModuleExport]
    public string? Directory(string path) => Path.GetDirectoryName(path);

    /// <summary>
    /// Gets the file name for the given path
    /// </summary>
    /// <param name="path">The path</param>
    /// <returns>The file name</returns>
    [ModuleExport]
    public string? FileName(string path) => Path.GetFileName(path);

    /// <summary>
    /// Gets the file name without the extension for the given path
    /// </summary>
    /// <param name="path">The path</param>
    /// <returns>The file name without the extension</returns>
    [ModuleExport]
    public string? FileNameWithoutExtension(string path) => Path.GetFileNameWithoutExtension(path);

    /// <summary>
    /// Gets the extension for the given path
    /// </summary>
    /// <param name="path">The path</param>
    /// <returns>The extension without the leading dot</returns>
    [ModuleExport]
    public string? Extension(string path) => Path.GetExtension(path)?.Trim('.');

    /// <summary>
    /// Gets the fully qualified absolute path for the given path
    /// </summary>
    /// <param name="path">The path</param>
    /// <returns>The fully qualified path</returns>
    [ModuleExport]
    public string Full(string path) => Path.GetFullPath(path);

    /// <summary>
    /// Gets the invalid path characters
    /// </summary>
    /// <returns>The invalid path characters</returns>
    [ModuleExport]
    public string[] InvalidPathChars() => Path.GetInvalidPathChars().Select(c => c.ToString()).ToArray();

    /// <summary>
    /// Gets the invalid file name characters
    /// </summary>
    /// <returns>The invalid file name characters</returns>
    [ModuleExport]
    public string[] InvalidFileChars() => Path.GetInvalidFileNameChars().Select(c => c.ToString()).ToArray();

    /// <summary>
    /// Gets the path root for the given path
    /// </summary>
    /// <param name="path">The path</param>
    /// <returns>The root path</returns>
    [ModuleExport]
    public string? Root(string path) => Path.GetPathRoot(path);

    /// <summary>
    /// Gets a random file name
    /// </summary>
    /// <returns>The random file name</returns>
    [ModuleExport]
    public string Random() => Path.GetRandomFileName();

    /// <summary>
    /// Gets the path relative to the given base path
    /// </summary>
    /// <param name="relativeTo">The base path</param>
    /// <param name="path">The path to check</param>
    /// <returns>The path relative to the given path</returns>
    [ModuleExport]
    public string Relative(string relativeTo, string path) => Path.GetRelativePath(relativeTo, path);

    /// <summary>
    /// Creates a temporary file and returns the path
    /// </summary>
    /// <returns>The temporary file path</returns>
    [ModuleExport]
    public string TempFile() => Path.GetTempFileName();

    /// <summary>
    /// Gets the temporary directory
    /// </summary>
    /// <returns>The temporary directory</returns>
    [ModuleExport]
    public string Temp() => Path.GetTempPath();

    /// <summary>
    /// Checks if the given path is fully qualified
    /// </summary>
    /// <param name="path">The path</param>
    /// <returns>Whether or not the given path is fully qualified</returns>
    [ModuleExport]
    public bool IsQualified(string path) => Path.IsPathFullyQualified(path);

    /// <summary>
    /// Checks if the given path is rooted
    /// </summary>
    /// <param name="path">The path</param>
    /// <returns>Whether or not the path is rooted</returns>
    [ModuleExport]
    public bool IsRooted(string path) => Path.IsPathRooted(path);
}
