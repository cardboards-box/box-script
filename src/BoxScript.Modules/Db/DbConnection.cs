using Dapper;
using Jint.Native;

namespace BoxScript.Modules.Db;

using Core;

/// <summary>
/// The base class for every provider added to the database module.
/// </summary>
/// <param name="_connection">The connection to the database</param>
public class DbConnection(IDbConnection _connection) : IDisposable
{
    /// <summary>
    /// Begin a database transaction
    /// </summary>
    /// <returns>The database transaction</returns>
    [ModuleExport]
    public virtual DbTransaction Transaction()
    {
        return new DbTransaction(_connection.BeginTransaction());
    }

    /// <summary>
    /// Creates an instance of the settings
    /// </summary>
    /// <returns>The settings instance</returns>
    [ModuleExport]
    public virtual DbQuerySettings Settings() => new();

    /// <summary>
    /// Execute a query that does not return any results
    /// </summary>
    /// <param name="query">The query to execute</param>
    /// <param name="settings">The settings for the query</param>
    /// <returns>The number of records modified</returns>
    [ModuleExport]
    public virtual Task<int> Execute(string query, DbQuerySettings? settings = null)
    {
        return _connection.ExecuteAsync(query, 
            settings?.Parameters, 
            settings?.Transaction?.Transaction, 
            settings?.CommandTimeoutSec,
            settings?.Type);
    }

    /// <summary>
    /// Execute a query that returns results
    /// </summary>
    /// <param name="query">The query to execute</param>
    /// <param name="settings">The settings for the query</param>
    /// <returns>The records read from the database</returns>
    [ModuleExport(type: typeof(Task<JsValue[]>))]
    public virtual async Task<dynamic[]> Query(string query, DbQuerySettings? settings = null)
    {
        return (await _connection.QueryAsync(query,
            settings?.Parameters,
            settings?.Transaction?.Transaction,
            settings?.CommandTimeoutSec,
            settings?.Type)).ToArray();
    }

    /// <summary>
    /// Executes a query and returns an iterator that can be used to read the results one by one
    /// </summary>
    /// <param name="query">The query to execute</param>
    /// <param name="settings">The settings for the query</param>
    /// <returns>The record iterator that reads from the database</returns>
    [ModuleExport(type: typeof(Task<IteratorProxy>))]
    public virtual async Task<ScriptProxy> QueryUnbuffered(string query, DbQuerySettings? settings = null)
    {
        var cmd = new CommandDefinition(query, 
            settings?.Parameters,
            settings?.Transaction?.Transaction,
            settings?.CommandTimeoutSec,
            settings?.Type,
            CommandFlags.None);
        var results = await _connection.QueryAsync(cmd);
        return new(new IteratorProxy(results.GetEnumerator()));
    }

    /// <summary>
    /// Execute a query that returns the first result or null
    /// </summary>
    /// <param name="query">The query to execute</param>
    /// <param name="settings">The settings for the query</param>
    /// <returns>The records read from the database</returns>
    [ModuleExport(type: typeof(Task<JsValue?>))]
    public virtual Task<dynamic?> FirstOrDefault(string query, DbQuerySettings? settings = null)
    {
        return _connection.QueryFirstOrDefaultAsync(query,
            settings?.Parameters,
            settings?.Transaction?.Transaction,
            settings?.CommandTimeoutSec,
            settings?.Type);
    }

    /// <summary>
    /// Executes a query that returns a scalar result
    /// </summary>
    /// <param name="query">The query to execute</param>
    /// <param name="settings">The settings for the query</param>
    /// <returns>The records read from the database</returns>
    [ModuleExport(type: typeof(Task<JsValue?>))]
    public virtual Task<dynamic?> Scalar(string query, DbQuerySettings? settings = null)
    {
        return _connection.ExecuteScalarAsync(query,
            settings?.Parameters,
            settings?.Transaction?.Transaction,
            settings?.CommandTimeoutSec,
            settings?.Type);
    }

    /// <summary>
    /// Executes a query that returns multiple result sets
    /// </summary>
    /// <param name="query">The query to execute</param>
    /// <param name="settings">The settings for the query</param>
    /// <returns>The records read from the database</returns>
    [ModuleExport(type: typeof(Task<DbQueryReader>))]
    public virtual async Task<ScriptProxy> Multiple(string query, DbQuerySettings? settings = null)
    {
        var reader = await _connection.QueryMultipleAsync(query,
            settings?.Parameters,
            settings?.Transaction?.Transaction,
            settings?.CommandTimeoutSec,
            settings?.Type);
        return new(new DbQueryReader(reader));
    }

    /// <summary>
    /// Disposes the database connection
    /// </summary>
    [ModuleExport]
    public void Dispose()
    {
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }
}
