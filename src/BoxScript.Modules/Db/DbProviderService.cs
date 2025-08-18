using Dapper;

namespace BoxScript.Modules.Db;

/// <summary>
/// The base class for every provider added to the database module.
/// </summary>
public abstract class DbProviderService
{
    /// <summary>
    /// Create the connection to the database
    /// </summary>
    /// <returns></returns>
    public abstract Task<IDbConnection> CreateConnection(string connectionString);
}
