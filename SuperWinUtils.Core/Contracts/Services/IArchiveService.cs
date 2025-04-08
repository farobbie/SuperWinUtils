namespace SuperWinUtils.Core.Contracts.Services;
public interface IArchiveService
{
    Task ExtractFileToAsync(string SourceFilePath, string destinationPath, IProgress<double> progress, CancellationToken cancellationToken);
}
