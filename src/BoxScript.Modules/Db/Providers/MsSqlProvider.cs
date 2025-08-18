using Microsoft.Data.SqlClient;

namespace BoxScript.Modules.Db.Providers;

/// <summary>
/// The Connection Provider for connecting to a Microsoft SQL Server database
/// </summary>
public class MsSqlProvider(
    ILogger _logger) : DbConnectionSettings(_logger)
{
    /// <inheritdoc />
    public override System.Data.Common.DbConnection CreateConnection()
    {
        return new SqlConnection(ConnectionString 
            ?? throw new InvalidOperationException("Connection string is not set."));
    }
}
