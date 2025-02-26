namespace SuperWinUtils.Core.Contracts.Services;
public interface IArchiveService
{
    Task ExtractFileToAsync(string sourceFilePath, string destinationPath, IProgress<double> progress, CancellationToken cancellationToken);
}
