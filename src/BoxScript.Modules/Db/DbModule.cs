namespace BoxScript.Modules.Db;

using Core;
using Providers;

/// <summary>
/// A module that provides database functionality to box-scripts
/// </summary>
[Module("db")]
public class DbModule(
    ILogger<DbModule> _logger) : IScriptModule
{
    /// <summary>
    /// Creates an instance of the MySQL connection settings module
    /// </summary>
    /// <returns>The connection instance that were created</returns>
    [ModuleExport]
    public MySqlProvider MySql() => new(_logger);

    /// <summary>
    /// Creates an instance of the PostgreSQL connection settings module
    /// </summary>
    /// <returns>The connection instance that were created</returns>
    [ModuleExport]
    public NpgsqlProvider Postgres() => new(_logger);

    /// <summary>
    /// Creates an instance of the SQL Server connection settings module
    /// </summary>
    /// <returns>The connection instance that were created</returns>
    [ModuleExport]
    public MsSqlProvider SqlServer() => new(_logger);

    /// <summary>
    /// Creates an instance of the SQLite connection settings module
    /// </summary>
    /// <returns>The connection instance that were created</returns>
    [ModuleExport]
    public SqliteProvider SQLite() => new(_logger);
}
