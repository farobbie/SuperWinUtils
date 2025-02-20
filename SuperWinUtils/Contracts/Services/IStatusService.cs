namespace SuperWinUtils.Contracts.Services;

public interface IStatusService
{
    IProgress<string> StatusProgress { get; set; }
    Task UpdateMessage(string message);
}
