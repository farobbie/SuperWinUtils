using SuperWinUtils.Core.Contracts.Services;
using SuperWinUtils.Core.Models;

namespace SuperWinUtils.Core.Services;
public class FileExchangeService : IFileExchangeService
{
    public IAsyncEnumerable<DownloadProgress> DownloadFileAsync(string sourceFileUrl, string sourceFilePath, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
