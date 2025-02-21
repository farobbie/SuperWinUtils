using CommunityToolkit.Mvvm.ComponentModel;
using SuperWinUtils.Contracts.Services;

namespace SuperWinUtils.ViewModels;

public partial class BaseViewModel : ObservableRecipient
{
    protected readonly IStatusService _statusService = App.GetService<IStatusService>();
    protected readonly IDialogService _dialogService = App.GetService<IDialogService>();

    [ObservableProperty]
    public partial string? Title { get; set; }

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    protected bool IsNotBusy => !IsBusy;

    public BaseViewModel()
    {
        
    }

    protected async Task ReportStatus(string statusMessage)
    {
        await _statusService.Report($"{Title}: {statusMessage}");
    }
}
