using CardboardBox.Extensions.Excel;
using CardboardBox.Extensions.Scripting;

namespace BoxScript.Core;

/// <summary>
/// The extension methods used for adding dependency injection (DI) support to the BoxScript framework.
/// </summary>
public static class DiExtensions
{
    /// <summary>
    /// Adds the core services required by the BoxScript framework to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add to</param>
    /// <returns>The service collection for fluent method chaining</returns>
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        return services
            .AddTemplatingServices()
            .AddSerilog()
            .AddExcel();
    }
}
