using Dapper;

namespace BoxScript.Modules.Db;

/// <summary>
/// Represents the parameters passed to a dapper query
/// </summary>
public class DbParameters
{
    private readonly DynamicParameters _parameters = new();
}
