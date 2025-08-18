using Microsoft.Data.Sqlite;

namespace BoxScript.Modules.Db.Providers;

/// <summary>
/// The Connection Provider for connecting to a SQLite database
/// </summary>
/// <param name="logger"></param>
public class SqliteProvider(
    ILogger logger) : DbConnectionSettings(logger)
{
    /// <inheritdoc />
    public override System.Data.Common.DbConnection CreateConnection()
    {
        return new SqliteConnection(
            ConnectionString ?? throw new InvalidOperationException("Connection string is not set."));
    }
}
