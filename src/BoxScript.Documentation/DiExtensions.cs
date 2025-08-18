namespace BoxScript.Documentation;

/// <summary>
/// The extension methods used for adding dependency injection (DI) support to the BoxScript framework.
/// </summary>
public static class DiExtensions
{
    /// <summary>
    /// Adds the documentation services to the service collection
    /// </summary>
    /// <param name="services">The service collection to add to</param>
    /// <returns>The service collection for fluent method chaining</returns>
    public static IServiceCollection AddDocumentationServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDocumentReflectionService, DocumentReflectionService>()
            .AddSingleton<IJsTypeService, JsTypeService>()
            .AddTransient<IIndexRenderService, IndexRenderService>();
    }
}
