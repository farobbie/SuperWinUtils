using SuperWinUtils.Contracts.Services;

namespace SuperWinUtils.Services;

public partial class StatusService : IStatusService
{
    public required IProgress<string> StatusProgress { get; set; }

    public async Task UpdateMessage(string message)
    {
        await Task.Yield();
        StatusProgress?.Report(message);
    }
}
