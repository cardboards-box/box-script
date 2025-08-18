namespace BoxScript.Modules;

using Core;
using Db;
using Http;
using IO;

/// <summary>
/// The extension methods used for adding dependency injection (DI) support to the BoxScript framework.
/// </summary>
public static class DiExtensions
{
    /// <summary>
    /// Adds a collection of common <see cref="IScriptModule"/>s
    /// </summary>
    /// <param name="services">The service collection to add to</param>
    /// <returns>The service collection for fluent method chaining</returns>
    public static IScriptEngineSettings AddCommonModules(this IScriptEngineSettings services)
    {
        return services
            .AddModule<LogModule>()

            .AddModule<FileModule>()
            .AddModule<DirectoryModule>()
            .AddModule<PathModule>()
            .AddEnum<System.IO.Compression.CompressionLevel>()
            .AddEnum<SearchOption>()

            .AddModule<JsonModule>()
            .AddModule<HttpModule>()
            .AddModule<ConfigModule>()

            .AddModule<DbModule>()
            .AddEnum<DbType>()
            .AddEnum<ParameterDirection>()
            .AddEnum<CommandType>()

            .AddServices(c =>
                c.AddHttpClient()
                 .AddTransient<IJsonModule, JsonModule>()
                 .AddTransient<IEnumReflectionService, EnumReflectionService>());
    }
}
