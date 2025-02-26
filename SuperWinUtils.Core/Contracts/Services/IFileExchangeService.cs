using SuperWinUtils.Core.Models;

namespace SuperWinUtils.Core.Contracts.Services;
public interface IFileExchangeService
{
    Task DownloadFileAsync(string sourceFileUrl, string sourceFilePath, IProgress<DownloadProgress> progress, CancellationToken cancellationToken);
}
