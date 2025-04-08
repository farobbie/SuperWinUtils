using SharpCompress.Archives;
using SuperWinUtils.Core.Contracts.Services;

namespace SuperWinUtils.Core.Services;
public class ArchiveService : IArchiveService
{
    public async Task ExtractFileToAsync(string SourceFilePath, string destinationPath, IProgress<double> progress, CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            using var archive = ArchiveFactory.Open(SourceFilePath);
            var totalEntries = archive.Entries.Count();
            var processedEntries = 0;
            foreach (var entry  in archive.Entries)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (!entry.IsDirectory)
                {
                    entry.WriteToDirectory(destinationPath, new SharpCompress.Common.ExtractionOptions
                    {
                        ExtractFullPath = true,
                        Overwrite = true
                    });
                }

                processedEntries++;
                progress.Report((double)processedEntries / totalEntries);
            }

        }, cancellationToken);
    }
}
