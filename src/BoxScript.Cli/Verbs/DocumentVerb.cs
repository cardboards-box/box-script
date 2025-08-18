namespace BoxScript.Cli.Verbs;

using Documentation;
using Jint;
using Modules;
using Services;

/// <summary>
/// Options for the "document" verb
/// </summary>
[Verb("document", HelpText = "Generates documentation for the given box-script file")]
public class DocumentOptions
{
    private const string DEFAULT_FILE = "index.d.ts";

    /// <summary>
    /// Where to write the file to
    /// </summary>
    [Option('f', "file", HelpText = "Where to write the file to", Default = DEFAULT_FILE)]
    public string File { get; set; } = DEFAULT_FILE;
}

internal class DocumentVerb(
    ILogger<DocumentVerb> logger,
    IScriptEngineService _engine) : BooleanVerb<DocumentOptions>(logger)
{
    public IIndexRenderService GetRenderEngine()
    {
        var settings = _engine.SettingsInstance()
            .AddCommonModules();
        var services = _engine.GenerateServiceCollection(settings)
            .AddSingleton(new Engine())
            .AddDocumentationServices();

        var provider = services.BuildServiceProvider();
        services.AddSingleton<IServiceProvider>(provider);
        return provider.GetRequiredService<IIndexRenderService>();
    }

    public override async Task<bool> Execute(DocumentOptions options, CancellationToken token)
    {
        var render = GetRenderEngine();

        _logger.LogInformation("Starting write of index file: {File}", options.File);
        using var io = File.Create(options.File);
        using var sw = new StreamWriter(io);
        await render.WriteIndex(sw, Constants.MODULE_NAME);
        await io.FlushAsync(token);
        _logger.LogInformation("Finished write of index file: {File}", options.File);
        return true;
    }
}
