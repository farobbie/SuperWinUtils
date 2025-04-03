using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using SuperWinUtils.Contracts.Services;
using Windows.Storage;

namespace SuperWinUtils.ViewModels;

public partial class BaseViewModel : ObservableRecipient
{
    protected readonly IStatusService _statusService = App.GetService<IStatusService>();
    protected readonly IDialogService _dialogService = App.GetService<IDialogService>();
    private readonly IThemeSelectorService _themeSelectorService = App.GetService<IThemeSelectorService>();

    [ObservableProperty]
    public partial string? Title { get; set; }

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    protected bool IsNotBusy => !IsBusy;

    public BaseViewModel()
    {
        
    }

    protected async Task ShowAlert(string message)
    {
        await _dialogService.ShowAlertDialogAsync(message);
    }

    protected async Task ShowTextImage(string message, BitmapImage image)
    {
        await _dialogService.ShowImageWithText(message, image);
    }

    protected async Task<IReadOnlyList<StorageFile>> LoadImagesAsync()
    {
        var files = await _dialogService.OpenImagesAsync();
        if (files.Count <= 0)
        {
            await ShowAlert("No files selected.");
        }
        return files;
    }

    protected async Task ReportStatus(string statusMessage)
    {
        await _statusService.Report($"{Title}: {statusMessage}");
    }

    protected async Task AddThemeActionAsync(Action<ElementTheme> themeAction)
    {
        _themeSelectorService.ThemeChanged += themeAction;

        await Task.CompletedTask;
    }
}
