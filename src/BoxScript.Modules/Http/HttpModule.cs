using Jint.Native;
using System.Net.Http.Handlers;

namespace BoxScript.Modules.Http;

using Core;
using System;

/// <summary>
/// A module that provides HTTP functionality to box-scripts
/// </summary>
[Module("http")]
public class HttpModule(
    HttpClient _client,
    IJsonModule _json,
    ILogger<HttpModule> _logger) : IScriptModule
{
    /// <summary>
    /// Creates an instance of the HTTP settings module
    /// </summary>
    /// <returns>The settings that were created</returns>
    [ModuleExport(type: typeof(HttpSettings))]
    public ScriptProxy Settings() => new(new HttpSettings(_json));

    /// <summary>
    /// Sends an HTTP request with the given settings and returns the response message
    /// </summary>
    /// <param name="settings">The settings for the request</param>
    /// <returns>The response</returns>
    [ModuleExport(type: typeof(Task<HttpResp>))]
    public async Task<ScriptProxy> Send(HttpSettings settings)
    {
        return new(await SendImplementation(settings));
    }

    /// <summary>
    /// Sends an HTTP request and returns the JSON response
    /// </summary>
    /// <param name="settings">The settings for the request</param>
    /// <returns>The response</returns>
    [ModuleExport(type: typeof(Task<object>))]
    public async Task<JsValue> SendJson(HttpSettings settings)
    {
        using var resp = await SendImplementation(settings);
        return await resp.AsJson();
    }

    /// <summary>
    /// Sends a GET request to the given URL
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="settings">The optional settings for the request</param>
    /// <returns>The response</returns>
    [ModuleExport(type: typeof(Task<HttpResp>))]
    public Task<ScriptProxy> Get(string url, HttpSettings? settings = null)
    {
        settings ??= new HttpSettings(_json);
        settings
            .SetUrl(url)
            .SetMethod("GET");
        return Send(settings);
    }

    /// <summary>
    /// Sends a GET request to the given URL and returns the JSON response
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="settings">The optional settings for the request</param>
    /// <returns>The response</returns>
    [ModuleExport(type: typeof(Task<object>))]
    public Task<JsValue> GetJson(string url, HttpSettings? settings = null)
    {
        settings ??= new HttpSettings(_json);
        settings
            .SetUrl(url)
            .SetMethod("GET");
        return SendJson(settings);
    }

    /// <summary>
    /// Sends a GET request to the given URL
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="settings">The optional settings for the request</param>
    /// <returns>The response</returns>
    [ModuleExport(type: typeof(Task<HttpResp>))]
    public Task<ScriptProxy> Delete(string url, HttpSettings? settings = null)
    {
        settings ??= new HttpSettings(_json);
        settings
            .SetUrl(url)
            .SetMethod("DELETE");
        return Send(settings);
    }

    /// <summary>
    /// Sends a DELETE request to the given URL and returns the JSON response
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="settings">The optional settings for the request</param>
    /// <returns>The response</returns>
    [ModuleExport(type: typeof(Task<object>))]
    public Task<JsValue> DeleteJson(string url, HttpSettings? settings = null)
    {
        settings ??= new HttpSettings(_json);
        settings
            .SetUrl(url)
            .SetMethod("DELETE");
        return SendJson(settings);
    }

    /// <summary>
    /// Sends a POST request to the given URL
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="body">The body data of the request</param>
    /// <param name="settings">The optional settings for the request</param>
    /// <returns>The response</returns>
    [ModuleExport(type: typeof(Task<HttpResp>))]
    public Task<ScriptProxy> Post(string url, [ModuleExport(type: typeof(object))] JsValue body, HttpSettings? settings = null)
    {
        settings ??= new HttpSettings(_json);
        settings
            .SetUrl(url)
            .SetMethod("POST")
            .JsonBody(body);
        return Send(settings);
    }

    /// <summary>
    /// Sends a POST request to the given URL and returns the JSON response
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="body">The body data of the request</param>
    /// <param name="settings">The optional settings for the request</param>
    /// <returns>The response</returns>
    [ModuleExport(type: typeof(Task<object>))]
    public Task<JsValue> PostJson(string url, [ModuleExport(type: typeof(object))] JsValue body, HttpSettings? settings = null)
    {
        settings ??= new HttpSettings(_json);
        settings
            .SetUrl(url)
            .SetMethod("POST")
            .JsonBody(body);
        return SendJson(settings);
    }

    /// <summary>
    /// Sends a PUT request to the given URL
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="body">The body data of the request</param>
    /// <param name="settings">The optional settings for the request</param>
    /// <returns>The response</returns>
    [ModuleExport(type: typeof(Task<HttpResp>))]
    public Task<ScriptProxy> Put(string url, [ModuleExport(type: typeof(object))] JsValue body, HttpSettings? settings = null)
    {
        settings ??= new HttpSettings(_json);
        settings
            .SetUrl(url)
            .SetMethod("PUT")
            .JsonBody(body);
        return Send(settings);
    }

    /// <summary>
    /// Sends a PUT request to the given URL
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="body">The body data of the request</param>
    /// <param name="settings">The optional settings for the request</param>
    /// <returns>The response</returns>
    [ModuleExport(type: typeof(Task<object>))]
    public Task<JsValue> PutJson(string url, [ModuleExport(type: typeof(object))] JsValue body, HttpSettings? settings = null)
    {
        settings ??= new HttpSettings(_json);
        settings
            .SetUrl(url)
            .SetMethod("PUT")
            .JsonBody(body);
        return SendJson(settings);
    }

    private HttpClient GetClient(HttpSettings settings, out HttpProgress progress)
    {
        progress = new(settings, _logger);
        progress.Register();

        if (!settings.LogDownload && !settings.LogUpload)
            return _client;

        var factory = new HttpClientHandler();
        settings.AddDisposer(factory);
        var handler = new ProgressMessageHandler(factory);
        settings.AddDisposer(handler);
        var client = new HttpClient(handler);
        settings.AddDisposer(client);

        if (settings.LogDownload)
            handler.HttpReceiveProgress += progress.Receive;
        if (settings.LogUpload)
            handler.HttpSendProgress += progress.Send;

        return client;
    }

    private static string DetermineUrl(HttpSettings settings)
    {
        string url = settings.Url;
        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            url = settings.BaseUrl.TrimEnd('/') + "/" + url;
        foreach (var (key, value) in settings.QueryParams)
            url = url.AppendQueryParam(key, value);
        return url;
    }

    private async Task<HttpResp> SendImplementation(HttpSettings settings)
    {
        var tsc = new CancellationTokenSource();
        settings.AddDisposer(tsc);
        if (settings.Timeout > TimeSpan.Zero)
            tsc.CancelAfter(settings.Timeout);

        var uri = DetermineUrl(settings);
        var method = new HttpMethod(settings.Method);
        var request = new HttpRequestMessage(method, uri);
        settings.AddDisposer(request);

        foreach (var (key, value) in settings.Headers)
            request.Headers.Add(key, value);

        if (settings.Body is not null)
            request.Content = settings.Body;
        
        var client = GetClient(settings, out var progress);
        var response = await client.SendAsync(request, tsc.Token);
        settings.AddDisposer(response);
        return new(settings, request, response, progress, _json);
    }
}
