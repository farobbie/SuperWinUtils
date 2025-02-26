using SuperWinUtils.Core.Models;

namespace SuperWinUtils.Core.Contracts.Services;
public interface IFileExchangeService
{
    public IAsyncEnumerable<DownloadProgress> DownloadFileAsync(string sourceFileUrl, string sourceFilePath, CancellationToken cancellationToken);
}
