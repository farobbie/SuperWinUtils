using SuperWinUtils.Core.Models;

namespace SuperWinUtils.Core.Contracts.Services;
public interface IFileExchangeService
{
    Task DownloadFileAsync(string sourceFileUrl, string SourceFilePath, IProgress<DownloadProgress> progress, CancellationToken cancellationToken);
}
