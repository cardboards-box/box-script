namespace BoxScript.Modules.Db;

using Core;

/// <summary>
/// Represents a database transaction.
/// </summary>
/// <param name="_transaction">The DB Transaction</param>
public class DbTransaction(
    IDbTransaction _transaction) : IDisposable
{
    internal IDbTransaction Transaction => _transaction;

    /// <summary>
    /// Commit the transaction
    /// </summary>
    [ModuleExport]
    public void Commit()
    {
        Transaction.Commit();
    }

    /// <summary>
    /// Rollback the transaction
    /// </summary>
    [ModuleExport]
    public void Rollback()
    {
        Transaction.Rollback();
    }

    /// <summary>
    /// Dispose the transaction
    /// </summary>
    [ModuleExport]
    public void Dispose()
    {
        Transaction.Dispose();
        GC.SuppressFinalize(this);
    }
}
