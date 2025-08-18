using BoxScript.Core;
using Jint;
using Jint.Native;

namespace BoxScript.Modules.Http;

internal delegate void HttpVoidDelegate();

/// <summary>
/// The settings for the HTTP module
/// </summary>
public class HttpSettings(
    IJsonModule _json)
{
    internal event HttpVoidDelegate OnDisposing = () => { };

    /// <summary>
    /// The method of the settings
    /// </summary>
    [ModuleExport]
    public string Method { get; set; } = "GET";

    /// <summary>
    /// The URL of the request
    /// </summary>
    [ModuleExport]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// The base URL for the request
    /// </summary>
    [ModuleExport]
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// The headers for the request
    /// </summary>
    [ModuleExport]
    public Dictionary<string, string> Headers { get; } = new(StringComparer.InvariantCultureIgnoreCase);

    /// <summary>
    /// The query parameters for the request
    /// </summary>
    [ModuleExport]
    public List<QueryParam> QueryParams { get; } = [];

    /// <summary>
    /// The status codes to expect from the request
    /// </summary>
    /// <remarks>Any code in this list will not result in an error</remarks>
    [ModuleExport]
    public List<CodeRange> ExpectStatusCodes { get; } = [new(200, 300)];

    /// <summary>
    /// The body of the HTTP request
    /// </summary>
    internal HttpContent? Body { get; set; }

    /// <summary>
    /// Sets the timeout for the HTTP request
    /// </summary>
    internal TimeSpan Timeout { get; set; } = TimeSpan.Zero;

    /// <summary>
    /// Whether or not to log the download progress of the HTTP request
    /// </summary>
    [ModuleExport]
    public bool LogDownload { get; set; } = false;

    /// <summary>
    /// Whether or not to log the upload progress during the HTTP request
    /// </summary>
    [ModuleExport]
    public bool LogUpload { get; set; } = false;

    /// <summary>
    /// Sets the timer for how often to log the download progress of the HTTP request
    /// </summary>
    /// <remarks>Set to 0 to disable</remarks>
    internal TimeSpan LogDownloadTimeout { get; set; } = TimeSpan.Zero;

    /// <summary>
    /// Sets the timer for how often to log the upload progress during the HTTP request
    /// </summary>
    /// <remarks>Set to 0 to disable</remarks>
    internal TimeSpan LogUploadTimeout { get; set; } = TimeSpan.Zero;

    /// <summary>
    /// A collection of disposables that will be disposed of when the request is complete
    /// </summary>
    internal List<IDisposable> Disposers { get; } = [];

    /// <summary>
    /// Sets the method of the HTTP request
    /// </summary>
    /// <param name="method">The HTTP method</param>
    /// <returns>The settings for fluent method chaining</returns>
    [ModuleExport]
    public HttpSettings SetMethod([ModuleExport(type: typeof(string))] JsValue method)
    {
        Method = method.AsString();
        return this;
    }

    /// <summary>
    /// Sets the URL of the HTTP request
    /// </summary>
    /// <param name="url">The URL</param>
    /// <returns>The settings for fluent method chaining</returns>
    [ModuleExport]
    public HttpSettings SetUrl([ModuleExport(type: typeof(string))] JsValue url)
    {
        Url = url.AsString();
        return this;
    }

    /// <summary>
    /// Sets the base URL for the HTTP request
    /// </summary>
    /// <param name="url">The URL</param>
    /// <returns>The settings for fluent method chaining</returns>
    [ModuleExport]
    public HttpSettings SetBaseUrl([ModuleExport(type: typeof(string))] JsValue url)
    {
        BaseUrl = url.AsString();
        return this;
    }

    /// <summary>
    /// Adds a query parameter to the HTTP request
    /// </summary>
    /// <param name="key">The key of the query parameter</param>
    /// <param name="value">The value of the query parameter</param>
    /// <returns>The settings for fluent method chaining</returns>
    [ModuleExport]
    public HttpSettings AddParam([ModuleExport(type: typeof(string))] JsValue key, [ModuleExport(type: typeof(string))] JsValue value)
    {
        QueryParams.Add(new(key.AsString(), value.AsString()));
        return this;
    }

    /// <summary>
    /// Adds a header to the HTTP request
    /// </summary>
    /// <param name="key">The key of the header</param>
    /// <param name="value">The value of the header</param>
    /// <returns>The settings for fluent method chaining</returns>
    [ModuleExport]
    public HttpSettings SetHeader([ModuleExport(type: typeof(string))] JsValue key, [ModuleExport(type: typeof(string))] JsValue value)
    {
        Headers[key.AsString()] = value.AsString();
        return this;
    }

    /// <summary>
    /// Sets the body to a JSON object
    /// </summary>
    /// <param name="body">The body to set</param>
    /// <returns>The settings for fluent method chaining</returns>
    [ModuleExport]
    public HttpSettings JsonBody([ModuleExport(type: typeof(object))] JsValue? body)
    {
        if (body is null || body.IsNull() || body.IsUndefined())
            Body = null;
        else if (body.IsString())
            Body = new StringContent(body.AsString());
        else
            Body = new StringContent(_json.Serialize(body));
        return SetHeader("Content-Type", "application/json");
    }

    /// <summary>
    /// Sets the body content to a file
    /// </summary>
    /// <param name="path">The path of the file</param>
    /// <param name="mpcName">The name of the multi-part form content header for the file</param>
    /// <returns>The settings for fluent method chaining</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file was not found</exception>
    [ModuleExport]
    public HttpSettings FileBody([ModuleExport(type: typeof(string))] JsValue path, [ModuleExport(type: typeof(string))] JsValue? mpcName = null)
    {
        var strPath = path.AsString();
        if (!File.Exists(strPath))
            throw new FileNotFoundException($"The file '{strPath}' does not exist.", strPath);

        var strMpcName = mpcName?.AsString() ?? "file";

        var io = File.OpenRead(strPath);
        var content = new StreamContent(io);
        var stream = new MultipartFormDataContent
        {
            { content, strMpcName, Path.GetFileName(strPath) }
        };
        Body = stream;
        return this;
    }

    /// <summary>
    /// Sets the timeout in seconds for the HTTP request
    /// </summary>
    /// <param name="seconds">The number of seconds</param>
    /// <returns>The settings for fluent method chaining</returns>
    [ModuleExport]
    public HttpSettings SetTimeoutSeconds([ModuleExport(type: typeof(double))] JsValue seconds)
    {
        var value = seconds.AsNumber();

        if (value < 0) value = 0;
        Timeout = TimeSpan.FromSeconds(value);
        return this;
    }

    /// <summary>
    /// Clears the expected HTTP response codes
    /// </summary>
    /// <returns>The settings for fluent method chaining</returns>
    [ModuleExport]
    public HttpSettings ClearExpectedCodes()
    {
        ExpectStatusCodes.Clear();
        return this;
    }

    /// <summary>
    /// Adds an expected HTTP response code or range of codes
    /// </summary>
    /// <param name="min">The min code to expect</param>
    /// <param name="max">The optional max code to expect</param>
    /// <returns>The settings for fluent method chaining</returns>
    /// <remarks><paramref name="max"/> is exclusive</remarks>
    [ModuleExport]
    public HttpSettings ExpectCode([ModuleExport(type: typeof(int))] JsValue min, [ModuleExport(type: typeof(int))] JsValue? max = null)
    {
        var minValue = (int)min.AsNumber();
        var maxValue = max is not null ? (int)max.AsNumber() : minValue;
        ExpectStatusCodes.Add(new(minValue, maxValue));
        return this;
    }

    /// <summary>
    /// Sets whether or not to log the download progress during the HTTP request
    /// </summary>
    /// <param name="log">Whether or not to log the download progress</param>
    /// <param name="seconds">How often to log the progress (in seconds)</param>
    /// <returns>The settings for fluent method chaining</returns>
    [ModuleExport]
    public HttpSettings LogDownloads(bool log, double seconds = 0)
    {
        LogDownload = log;
        LogDownloadTimeout = seconds <= 0 ? TimeSpan.Zero : TimeSpan.FromSeconds(seconds);
        return this;
    }

    /// <summary>
    /// Sets whether or not to log the upload progress during the HTTP request
    /// </summary>
    /// <param name="log">Whether or not to log the upload progress</param>
    /// <param name="seconds">How often to log the progress (in seconds)</param>
    /// <returns>The settings for fluent method chaining</returns>
    [ModuleExport]
    public HttpSettings LogUploads(bool log, double seconds = 0)
    {
        LogUpload = log;
        LogUploadTimeout = seconds <= 0 ? TimeSpan.Zero : TimeSpan.FromSeconds(seconds);
        return this;
    }

    /// <inheritdoc />
    internal void DisposeChildren()
    {
        OnDisposing();
        foreach (var disposer in Disposers)
        {
            try
            {
                disposer.Dispose();
            }
            catch { }
        }
        Disposers.Clear();
    }

    internal void AddDisposer(IDisposable disposer)
    {
        if (disposer is null) return;
        Disposers.Add(disposer);
    }

    /// <summary>
    /// Represents a query parameter for an HTTP request
    /// </summary>
    /// <param name="Key">The key of the request</param>
    /// <param name="Value">The value of the request</param>
    public record class QueryParam(
        [property: ModuleExport] string Key,
        [property: ModuleExport] string? Value);

    /// <summary>
    /// Represents a range of HTTP status codes
    /// </summary>
    /// <param name="Min">The minimum value</param>
    /// <param name="Max">The exclusive maximum value</param>
    public record class CodeRange(
        [property: ModuleExport] int Min,
        [property: ModuleExport] int Max);
}
