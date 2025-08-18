using MySql.Data.MySqlClient;

namespace BoxScript.Modules.Db.Providers;

/// <summary>
/// The Connection Provider for connecting to a MySQL database
/// </summary>
public class MySqlProvider(
    ILogger logger) : DbConnectionSettings(logger)
{
    /// <inheritdoc />
    public override System.Data.Common.DbConnection CreateConnection()
    {
        return new MySqlConnection(
            ConnectionString ?? throw new InvalidOperationException("Connection string is not set."));
    }
}
