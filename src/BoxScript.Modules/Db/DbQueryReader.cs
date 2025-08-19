using Dapper;

namespace BoxScript.Modules.Db;

using Core;

/// <summary>
/// A reader for multi-return results query
/// </summary>
public class DbQueryReader(
    SqlMapper.GridReader _reader) : IDisposable
{
    /// <summary>
    /// Reads the next result set from the query
    /// </summary>
    /// <returns>The records from the reader</returns>
    [ModuleExport(type: typeof(Task<JsValue[]>))]
    public async Task<dynamic[]> Read()
    {
        return (await _reader.ReadAsync()).ToArray();
    }

    /// <summary>
    /// Reads the next result set from the query and returns an iterator proxy
    /// </summary>
    /// <returns>The record iterator</returns>
    [ModuleExport(type: typeof(IteratorProxyAsync))]
    public ScriptProxy ReadUnbuffered()
    {
        var records = _reader.ReadUnbufferedAsync();
        return new(new IteratorProxyAsync(records.GetAsyncEnumerator()));
    }

    /// <summary>
    /// Reads the next result set from the query and returns the first record or null
    /// </summary>
    /// <returns>The record or null</returns>
    [ModuleExport(type: typeof(Task<JsValue?>))]
    public Task<dynamic?> FirstOrDefault()
    {
        return _reader.ReadFirstOrDefaultAsync();
    }

    /// <summary>
    /// Reads the next result set from the query and returns the first record or null
    /// </summary>
    /// <returns>The record or null</returns>
    [ModuleExport(type: typeof(Task<JsValue>))]
    public Task<dynamic> Single()
    {
        return _reader.ReadSingleAsync();
    }

    /// <summary>
    /// Disposes the database connection
    /// </summary>
    [ModuleExport]
    public void Dispose()
    {
        _reader.Dispose();
        GC.SuppressFinalize(this);
    }
}
