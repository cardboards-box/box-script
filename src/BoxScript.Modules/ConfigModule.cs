namespace BoxScript.Modules;

using Core;

/// <summary>
/// A module that provides configuration functionality to box-scripts
/// </summary>
[Module("config")]
public class ConfigModule(
    IConfiguration _config) : IScriptModule
{
    /// <summary>
    /// Gets the value of the given configuration key
    /// </summary>
    /// <param name="key">The configuration key</param>
    /// <returns>The configuration value</returns>
    [ModuleExport]
    public string? Get(string key) => _config[key];
}
