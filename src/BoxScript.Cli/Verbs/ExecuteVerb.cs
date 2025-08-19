using Serilog;

namespace BoxScript.Cli.Verbs;

using Core;
using Modules;
using Services;

/// <summary>
/// Executes the given box-script file
/// </summary>
[Verb("execute", isDefault: true, HelpText = "Executes the given box-script file")]
public class ExecuteOptions
{
    /// <summary>
    /// The box-script file to execute
    /// </summary>
    [Option('f', "file", HelpText = "The box-script file to execute")]
    public string? File { get; set; }

    /// <summary>
    /// The inline box-script code to execute
    /// </summary>
    [Value(0, HelpText = "The inline box-script code to execute", Required = false)]
    public IEnumerable<string> Script { get; set; } = [];

    /// <summary>
    /// The configuration file to use
    /// </summary>
    [Option('c', "config-file", HelpText = "The configuration file to use - Supports XML, JSON, and INI files (relative to the script or working-directory)")]
    public string? ConfigFile { get; set; }

    /// <summary>
    /// The log file to write the execution logs to
    /// </summary>
    [Option('l', "log-file", HelpText = "The log file to write the execution logs to (relative to the script or working-directory)")]
    public string? LogFile { get; set; }

    /// <summary>
    /// Sets the working directory for relative path resolution
    /// </summary>
    [Option('w', "working-directory", HelpText = "Sets the working directory for relative path resolution")]
    public string? WorkingDirectory { get; set; }

    /// <summary>
    /// The timeout in seconds for the script execution
    /// </summary>
    [Option('t', "timeout-seconds", HelpText = "The timeout in seconds for the script execution")]
    public double? TimeoutSeconds { get; set; }

    /// <summary>
    /// The maximum number of recursive calls allowed in the script
    /// </summary>
    [Option('r', "recursion-limit", HelpText = "The maximum number of recursive calls allowed in the script")]
    public int? RecursionLimit { get; set; }

    /// <summary>
    /// The maximum memory limit in MB for the script execution
    /// </summary>
    [Option('m', "memory-limit-mb", HelpText = "The maximum memory limit in MB for the script execution")]
    public double? MemoryLimitMb { get; set; }
}

internal class ExecuteVerb(
    ILogger<ExecuteVerb> logger,
    IScriptEngineService _engine) : BooleanVerb<ExecuteOptions>(logger)
{
    public IScriptEngineSettings GetSettings(ExecuteOptions options, CancellationToken token)
    {
        var settings = _engine.SettingsInstance()
            .SetCancelToken(token);

        if (!string.IsNullOrEmpty(options.ConfigFile))
        {
            if (!File.Exists(options.ConfigFile))
            {
                _logger.LogError("The configuration file '{ConfigFile}' does not exist.", options.ConfigFile);
                throw new FileNotFoundException("Configuration file not found", options.ConfigFile);
            }

            settings.AddConfigFile(options.ConfigFile);
        }

        if (!string.IsNullOrEmpty(options.LogFile))
            settings.AddLogger(c => c
                .WriteTo.File(options.LogFile, rollingInterval: RollingInterval.Day));

        if (options.TimeoutSeconds is not null)
            settings.SetExecutionTimeout(options.TimeoutSeconds.Value);

        if (options.RecursionLimit is not null)
            settings.SetRecursionLimit(options.RecursionLimit.Value);

        if (options.MemoryLimitMb is not null)
            settings.SetMemoryLimit(options.MemoryLimitMb.Value);

        return settings
            .AddCommonModules();
    }

    public void UpdateWorkingDirectory(ExecuteOptions options, bool hasFile)
    {
        var dir = options.WorkingDirectory?.ForceNull();
        if (string.IsNullOrEmpty(dir) && hasFile)
            dir = options.File!;

        var directory = Path.GetDirectoryName(dir);
        if (string.IsNullOrEmpty(directory)) return;

        var full = Path.GetFullPath(directory);
        Directory.SetCurrentDirectory(full);
        _logger.LogInformation("Set working directory to: {dir}", full);
    }

    public static string? GetInline(ExecuteOptions options)
    {
        if (options.Script is null || !options.Script.Any())
            return null;

        return string.Join(" ", options.Script)?.ForceNull();
    }

    public override async Task<bool> Execute(ExecuteOptions options, CancellationToken token)
    {
        var inlineScript = GetInline(options);
        var hasInline = !string.IsNullOrWhiteSpace(inlineScript);
        var hasFile = !string.IsNullOrWhiteSpace(options.File);
        if (!hasInline && !hasFile)
        {
            _logger.LogError("You must provide either a script or a file to execute.");
            return false;
        }

        if (hasInline && hasFile)
        {
            _logger.LogError("You cannot provide both a script and a file to execute.");
            return false;
        }

        if (hasFile && !File.Exists(options.File!))
        {
            _logger.LogError("The file '{File}' does not exist.", options.File);
            return false;
        }

        var script = hasInline
            ? inlineScript!
            : await File.ReadAllTextAsync(options.File!, token);
        var settings = GetSettings(options, token);
        UpdateWorkingDirectory(options, hasFile);
        _logger.LogInformation("Starting to execute script >> {script}", script.SafeSubString(400));
        var result = await _engine.Execute(script, settings);
        if (result is null || result.IsUndefined() || result.IsNull())
        {
            _logger.LogInformation("Script executed successfully with no return value.");
            return true;
        }

        _logger.LogInformation("Script executed successfully with return value: {result}", result.ToJson());
        return true;
    }
}
