namespace BoxScript.Modules.Db;

using Core;

/// <summary>
/// A module that provides database functionality to box-scripts
/// </summary>
/// <param name="_logger"></param>
[Module("db")]
public class DbModule(
    ILogger<DbModule> _logger) : IScriptModule
{
}
