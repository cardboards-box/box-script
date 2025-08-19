namespace BoxScript.Modules.Http;

using Core;
using IO;

/// <summary>
/// A wrapper for the results of an HTTP request
/// </summary>
public class HttpResp(
    HttpSettings _settings,
    HttpRequestMessage _request,
    HttpResponseMessage _response,
    HttpProgress _progress,
    IJsonModule _json) : IDisposable
{
    /// <summary>
    /// The URL of the request
    /// </summary>
    [ModuleExport]
    public string Url => _request.RequestUri?.ToString() ?? _settings.Url;

    /// <summary>
    /// The response code
    /// </summary>
    [ModuleExport]
    public int Code => (int)_response.StatusCode;

    /// <summary>
    /// Whether or not the response status code is within the expected range.
    /// </summary>
    [ModuleExport]
    public bool Valid => CodeIsInRange(Code);

    /// <summary>
    /// The response headers
    /// </summary>
    [ModuleExport]
    public Dictionary<string, string> Headers => _response.Headers.ToDictionary(
        h => h.Key, 
        h => string.Join(", ", h.Value), 
        StringComparer.InvariantCultureIgnoreCase);

    /// <summary>
    /// The progress of the request
    /// </summary>
    [ModuleExport]
    public HttpProgress Progress => _progress;

    /// <summary>
    /// Throws an exception if the response status code is not in the expected range.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="HttpRequestException">Thrown if the code isn't within the expected range</exception>
    [ModuleExport]
    public async Task ThrowIfBad()
    {
        if (Valid) return;

        var message = $"Unexpected status code {_response.StatusCode} for {_settings.Method} {_settings.Url}";
        if (_response.Content is not null)
            message += $": {await _response.Content.ReadAsStringAsync()}";
        throw new HttpRequestException(message);
    }

    /// <summary>
    /// Returns the response body as a JSON value
    /// </summary>
    /// <returns>The response body as a JSON value</returns>
    [ModuleExport]
    public async Task<JsValue> AsJson()
    {
        if (_response.Content is null)
            return JsValue.Undefined;
        var content = await _response.Content.ReadAsStringAsync();
        return _json.Deserialize(content);
    }

    /// <summary>
    /// Returns the response body as a string
    /// </summary>
    /// <returns>The response body as a string</returns>
    [ModuleExport]
    public async Task<string> AsString()
    {
        if (_response.Content is null)
            return string.Empty;
        return await _response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Returns the response body as a stream
    /// </summary>
    /// <returns></returns>
    [ModuleExport(type: typeof(Task<StreamReaderProxy>))]
    public async Task<ScriptProxy> AsStream()
    {
        var content = await _response.Content.ReadAsStreamAsync();
        return new(new StreamReaderProxy(content));
    }

    /// <summary>
    /// Writes the response body to a file
    /// </summary>
    /// <param name="path">The file path</param>
    /// <exception cref="InvalidOperationException">Thrown if there is no response body</exception>
    [ModuleExport]
    public async Task ToFile(string path)
    {
        if (_response.Content is null)
            throw new InvalidOperationException("Response has no content to write to file.");
        await using var fs = File.Create(path);
        await _response.Content.CopyToAsync(fs);
        await fs.FlushAsync();
    }

    /// <inheritdoc />
    [ModuleExport]
    public void Dispose()
    {
        _settings.DisposeChildren();
        GC.SuppressFinalize(this);
    }

    private bool CodeIsInRange(int code)
    {
        if (_settings.ExpectStatusCodes.Count == 0)
            return true;

        foreach (var (min, max) in _settings.ExpectStatusCodes)
            if (code >= min && code < max)
                return true;

        return false;
    }
}
