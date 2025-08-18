using BoxScript.Cli.Verbs;
using BoxScript.Core;
using BoxScript.Services;

return await new ServiceCollection()
    .AddCoreServices()
    .AddScriptingServices()
    .Cli(c => c
        .Add<ExecuteVerb>()
        .Add<DocumentVerb>());