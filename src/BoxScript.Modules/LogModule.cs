namespace BoxScript.Modules;

using Core;

/// <summary>
/// A module that provides logging functionality to box-scripts
/// </summary>
[Module("logger")]
public class LogModule(
    ILogger<LogModule> _logger) : IScriptModule
{
    #pragma warning disable CA2254 // Template should be a static expression
    /// <summary>
    /// Logs an informational message with the given arguments
    /// </summary>
    /// <param name="message">The log message</param>
    /// <param name="args">The log arguments</param>
    [ModuleExport]
    public void Trace(string message, params object[] args)
    {
        _logger.LogTrace(message, args);
    }

    /// <summary>
    /// Logs a debug message with the given arguments
    /// </summary>
    /// <param name="message">The log message</param>
    /// <param name="args">The log arguments</param>
    [ModuleExport]
    public void Debug(string message, params object[] args)
    {
        _logger.LogDebug(message, args);
    }

    /// <summary>
    /// Logs an informational message with the given arguments
    /// </summary>
    /// <param name="message">The log message</param>
    /// <param name="args">The log arguments</param>
    [ModuleExport]
    public void Info(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    /// <summary>
    /// Logs a warning message with the given arguments
    /// </summary>
    /// <param name="message">The log message</param>
    /// <param name="args">The log arguments</param>
    [ModuleExport]
    public void Warning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }

    /// <summary>
    /// Logs an error message with the given arguments
    /// </summary>
    /// <param name="message">The log message</param>
    /// <param name="args">The log arguments</param>
    [ModuleExport]
    public void Error(string message, params object[] args)
    {
        _logger.LogError(message, args);
    }

    /// <summary>
    /// Logs a critical error message with the given arguments
    /// </summary>
    /// <param name="message">The log message</param>
    /// <param name="args">The log arguments</param>
    [ModuleExport]
    public void Critical(string message, params object[] args)
    {
        _logger.LogCritical(message, args);
    }
    #pragma warning restore CA2254 // Template should be a static expression
}
