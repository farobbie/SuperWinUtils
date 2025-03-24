using System.Runtime.CompilerServices;
using SuperWinUtils.Core.Contracts.Services;
using SuperWinUtils.Core.Models;

namespace SuperWinUtils.Core.Services;
public class FileExchangeService (HttpClient httpClient) : IFileExchangeService
{
    private readonly HttpClient _httpClient = httpClient;
    public async Task DownloadFileAsync(string sourceFileUrl, string sourceFilePath, IProgress<DownloadProgress> progress, CancellationToken cancellationToken)
    {
        var existingLength = File.Exists(sourceFilePath) ? new FileInfo(sourceFilePath).Length : 0;

        using var request = new HttpRequestMessage(HttpMethod.Get, sourceFileUrl);
        if (existingLength > 0)
        {
            request.Headers.Range = new System.Net.Http.Headers.RangeHeaderValue(existingLength, null);            
        }

        using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        response.EnsureSuccessStatusCode();

        var totalBytes = response.Content.Headers.ContentRange?.Length ?? response.Content.Headers.ContentLength ?? 0L;
        var buffer = new byte[8192];

        await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        await using var fileStream = new FileStream(
            sourceFilePath,
            existingLength > 0 ? FileMode.Append : FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            buffer.Length,
            FileOptions.Asynchronous | FileOptions.SequentialScan
            );

        var downloadProgress = new DownloadProgress
        {
            TotalBytes = totalBytes,
            BytesReceived = existingLength
        };

        int bytesRead;
        while ((bytesRead = await contentStream.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken)) > 0)
        {
            await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);

            downloadProgress.BytesReceived += bytesRead;
            progress?.Report(downloadProgress);
        }
    }
}
