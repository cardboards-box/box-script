using Dapper;
using Jint.Native;

namespace BoxScript.Modules.Db;

using Core;
using Jint;

/// <summary>
/// The settings for a query
/// </summary>
public class DbQuerySettings
{
    internal DynamicParameters Parameters { get; set; } = new();
    internal DbTransaction? Transaction { get; set; }

    /// <summary>
    /// The command timeout in seconds
    /// </summary>
    [ModuleExport]
    public int CommandTimeoutSec { get; set; } = 30;

    /// <summary>
    /// The command type of the transaction
    /// </summary>
    [ModuleExport]
    public CommandType Type { get; set; } = CommandType.Text;

    /// <summary>
    /// Sets the command timeout in seconds
    /// </summary>
    /// <param name="seconds">The number of seconds to wait</param>
    /// <returns>The current settings for fluent chaining</returns>
    [ModuleExport]
    public DbQuerySettings WithCommandTimeout(int seconds)
    {
        CommandTimeoutSec = seconds;
        return this;
    }

    /// <summary>
    /// Sets the transaction for the query
    /// </summary>
    /// <param name="transaction">The DB transaction</param>
    /// <returns>The current settings for fluent chaining</returns>
    [ModuleExport]
    public DbQuerySettings WithTransaction(DbTransaction transaction)
    {
        Transaction = transaction;
        return this;
    }

    /// <summary>
    /// Sets the command type for the query
    /// </summary>
    /// <param name="type">The command type</param>
    /// <returns>The current settings for fluent chaining</returns>
    [ModuleExport]
    public DbQuerySettings WithType(CommandType type)
    {
        Type = type;
        return this;
    }

    /// <summary>
    /// Adds a parameter to the query settings
    /// </summary>
    /// <param name="name">The name of the parameter</param>
    /// <param name="value">The value of the parameter</param>
    /// <param name="type">The type of the parameter</param>
    /// <param name="direction">The direction of the parameter</param>
    /// <returns>The current settings for fluent chaining</returns>
    [ModuleExport]
    public DbQuerySettings AddParameter(string name, JsValue value, DbType? type = null, ParameterDirection? direction = null)
    {
        var obj = value.ToObject();
        Parameters.Add(name, obj, type, direction);
        return this;
    }

    /// <summary>
    /// Adds multiple parameters to the query settings from an object
    /// </summary>
    /// <param name="value">The parameters</param>
    /// <returns>The current settings for fluent chaining</returns>
    [ModuleExport]
    public DbQuerySettings AddParameters(JsValue value)
    {
        if (!value.IsObject())
            throw new ArgumentException("Value must be an object", nameof(value));

        var obj = value.AsObject();
        foreach(var prop in obj.GetOwnProperties())
        {
            var par = prop.Key.ToString();
            var name = prop.Value.Value.ToObject();
            Parameters.Add(par, name);
        }
        return this;
    }
}
