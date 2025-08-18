using BoxScript.Core;
using System.Diagnostics;
using System.Net.Http.Handlers;

namespace BoxScript.Modules.Http;

/// <summary>
/// A class that holds the progress of an HTTP request.
/// </summary>
public class HttpProgress(
    HttpSettings _settings,
    ILogger _logger)
{
    private long? _downloadBytes = null;
    private int? _downloadPercentage = null;
    private readonly Stopwatch _downloadTimer = new();
    private readonly CancellationTokenSource _downloadCanceller = new();

    private long? _uploadBytes = null;
    private int? _uploadPercentage = null;
    private readonly Stopwatch _uploadTimer = new();
    private readonly CancellationTokenSource _uploadCanceller = new();

    /// <summary>
    /// The amount of bytes uploaded
    /// </summary>
    [ModuleExport]
    public double UploadedMb => (_uploadBytes ?? 0) / 1024.0 / 1024.0;

    /// <summary>
    /// The percentage of the upload that has been completed
    /// </summary>
    [ModuleExport]
    public int UploadPercentage => _uploadPercentage ?? 0;

    /// <summary>
    /// The time it took to upload the data
    /// </summary>
    [ModuleExport]
    public double UploadTimeSeconds => _uploadTimer.Elapsed.TotalSeconds;

    /// <summary>
    /// Whether or not the upload has finished
    /// </summary>
    [ModuleExport]
    public bool UploadFinished => _uploadPercentage >= 100;

    /// <summary>
    /// The amount of bytes downloaded
    /// </summary>
    [ModuleExport]
    public double DownloadedMb => (_downloadBytes ?? 0) / 1024.0 / 1024.0;

    /// <summary>
    /// The percentage of the download that has been completed
    /// </summary>
    [ModuleExport]
    public int DownloadPercentage => _downloadPercentage ?? 0;

    /// <summary>
    /// The time it took to download the data
    /// </summary>
    [ModuleExport]
    public double DownloadTimeSeconds => _downloadTimer.Elapsed.TotalSeconds;

    /// <summary>
    /// Whether or not the download has finished
    /// </summary>
    [ModuleExport]
    public bool DownloadFinished => _downloadPercentage >= 100;

    /// <summary>
    /// The settings for the request
    /// </summary>
    [ModuleExport]
    public HttpSettings Settings => _settings;

    internal void Register()
    {
        Settings.AddDisposer(_downloadCanceller);
        Settings.AddDisposer(_uploadCanceller);
        Settings.OnDisposing += Finish;
    }

    internal void Finish()
    {
        _downloadTimer.Stop();
        _downloadCanceller.Cancel();
        _uploadTimer.Stop();
        _uploadCanceller.Cancel();
        Settings.OnDisposing -= Finish;
    }

    internal void Log(bool download)
    {
        if (download)
        {
            _logger.LogInformation("Download progress: {Bytes:00.0000}Mb ({Percentage}%) in {Elapsed:00.00} seconds",
                DownloadedMb, DownloadPercentage, DownloadTimeSeconds);
            return;
        }

        _logger.LogInformation("Upload progress: {Bytes:00.0000}Mb ({Percentage}%) in {Elapsed:00.00} seconds",
                UploadedMb, UploadPercentage, UploadTimeSeconds);
    }

    internal void Receive(object? _, HttpProgressEventArgs e)
    {
        //If this is the first download event, start the timer
        if (!_downloadBytes.HasValue)
        {
            _downloadTimer.Start();
            StartProgress(true);
        }
        //Update the progress
        _downloadBytes = e.BytesTransferred;
        _downloadPercentage = e.ProgressPercentage;
        //Stop the download time if this is the last event
        if (DownloadFinished)
        {
            _downloadTimer.Stop();
            _downloadCanceller.Cancel();
        }
        //If timed progress reports are enabled - skip the rest
        if (_settings.LogDownloadTimeout > TimeSpan.Zero)
            return;
        Log(true);
    }

    internal void Send(object? _, HttpProgressEventArgs e)
    {
        //If this is the first upload event, start the timer
        if (!_uploadBytes.HasValue)
        {
            _uploadTimer.Start();
            StartProgress(false);
        }
        //Update the progress
        _uploadBytes = e.BytesTransferred;
        _uploadPercentage = e.ProgressPercentage;
        //Stop the upload time if this is the last event
        if (UploadFinished)
        {
            _uploadTimer.Stop();
            _uploadCanceller.Cancel();
        }
        //If timed progress reports are enabled - skip the rest
        if (_settings.LogUploadTimeout > TimeSpan.Zero)
            return;
        Log(false);
    }

    internal void StartProgress(bool download)
    {
        var token = download ? _downloadCanceller.Token : _uploadCanceller.Token;
        _ = Task.Run(() => ProgressReport(download, token), token);
    }

    internal async Task ProgressReport(bool download, CancellationToken token)
    {
        try
        {
            var timeout = download
                ? Settings.LogDownloadTimeout
                : Settings.LogUploadTimeout;
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(timeout, token);
                Log(download);
            }
        }
        catch (TaskCanceledException) { }
    }
}
