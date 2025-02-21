using SuperWinUtils.Contracts.Services;

namespace SuperWinUtils.Services;

public partial class StatusService : IStatusService
{
    public required IProgress<string> StatusProgress { get; set; }

    public Task Report(string message)
    {
        StatusProgress?.Report(message);
        return Task.CompletedTask;
    }
}
