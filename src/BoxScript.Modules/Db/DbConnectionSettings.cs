namespace BoxScript.Modules.Db;

using Core;
using Con = System.Data.Common.DbConnection;

/// <summary>
/// The settings for connecting to the database
/// </summary>
public abstract class DbConnectionSettings(
    ILogger _logger) : IScriptModule
{
    internal TimeSpan ConnectRetryTimeout { get; set; } = TimeSpan.FromSeconds(5);

    /// <summary>
    /// The number of times to attempt to connect to the server
    /// </summary>
    [ModuleExport]
    public int ConnectRetries { get; set; } = 3;

    /// <summary>
    /// The timeout between connection retries in seconds
    /// </summary>
    [ModuleExport]
    public double ConnectRetryTimeoutSec => ConnectRetryTimeout.TotalSeconds;

    /// <summary>
    /// The connection string to use
    /// </summary>
    [ModuleExport]
    public string? ConnectionString { get; set; }

    /// <summary>
    /// Sets the connection string to use for the database connection
    /// </summary>
    /// <param name="conString">The connection string</param>
    /// <returns>The current settings for fluent chaining</returns>
    [ModuleExport(type: typeof(DbConnectionSettings))]
    public ScriptProxy WithConnection(string conString)
    {
        ConnectionString = conString;
        return new(this);
    }

    /// <summary>
    /// Sets the number of times to attempt to connect to the server
    /// </summary>
    /// <param name="count">The number of retries</param>
    /// <returns>The current settings for fluent chaining</returns>
    [ModuleExport(type: typeof(DbConnectionSettings))]
    public ScriptProxy WithConnectRetryCount(int count)
    {
        ConnectRetries = count;
        return new(this);
    }

    /// <summary>
    /// Sets the timeout between connection retries in seconds
    /// </summary>
    /// <param name="seconds">The number of seconds to wait</param>
    /// <returns>The current settings for fluent chaining</returns>
    [ModuleExport(type: typeof(DbConnectionSettings))]
    public ScriptProxy WithConnectRetryTimeout(double seconds)
    {
        ConnectRetryTimeout = TimeSpan.FromSeconds(seconds);
        return new(this);
    }

    /// <summary>
    /// Creates a new database connection using the provided connection string and settings.
    /// </summary>
    /// <returns>The connection</returns>
    public abstract Con CreateConnection();

    /// <summary>
    /// Connects to the database using the provided connection string and settings.
    /// </summary>
    /// <returns>The open connection</returns>
    [ModuleExport(type: typeof(Task<DbConnection>))]
    public async Task<ScriptProxy> Connect()
    {
        var con = CreateConnection();
        int retries = ConnectRetries;

        //Keep retrying the connection until it works or it exceeds the max retry count
        while(con.State != ConnectionState.Open)
        {
            try
            {
                retries--;
                await con.OpenAsync();

                //This is to prevent the possibility of an infinite loop. This should never
                //occur as `await con.OpenAsync()` should throw an exception if it fails to open the connection.
                //but ya never know with Microsoft or other ADO.Net providers...
                if (con.State != ConnectionState.Open)
                    throw new Exception("SQL connection was opened successfully " +
                        "(we passed `await con.OpenAsync()`), but the connection state was not open");
            }
            catch (Exception ex)
            {
                //Log the exception
                _logger.LogError(ex, "Failed to connect to the database - Current Retry Count: {count} >> {string}", 
                    ConnectRetries - retries, ConnectionString);

                //Exceeded max retry count? Bubble up the error
                if (retries <= 0)
                    throw;
                //Wait a while before retrying connection
                await Task.Delay(ConnectRetryTimeout);
            }
        }

        return new(new DbConnection(con));
    }
}
