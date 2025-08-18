using Npgsql;

namespace BoxScript.Modules.Db.Providers;

/// <summary>
/// The Connection Provider for connecting to a PostgreSQL database
/// </summary>
public class NpgsqlProvider(
    ILogger logger) : DbConnectionSettings(logger)
{
    /// <inheritdoc />
    public override System.Data.Common.DbConnection CreateConnection()
    {
        return new NpgsqlConnection(
            ConnectionString ?? throw new InvalidOperationException("Connection string is not set."));
    }
}
