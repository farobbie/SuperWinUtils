using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperWinUtils.Contracts.Services;
using SuperWinUtils.Helpers;

namespace SuperWinUtils.ViewModels;

public partial class MuseScoreViewModel : BaseViewModel
{
    private readonly ILocalSettingsService _localSettingsService;

    private const string _settingsKeySourceFileUrl = "MuseScore_SourceFileUrl";
    private const string _settingsKeySourceFilePath = "MuseScore_SourceFilePath";
    private const string _settingsKeyDestinationFilePath = "MuseScore_DestinationFilePath";

    private readonly string _defaultSourceFileUrl = "MuseScore_SourceFileUrl_Default".GetLocalized() ?? "";
    private readonly string _defaultSourceFilePath = "MuseScore_SourceFilePath_Default".GetLocalized() ?? "";
    private readonly string _defaultDestinationFilePath = "MuseScore_DestinationFilePath_Default".GetLocalized() ?? "";

    [ObservableProperty]
    public partial string SourceFileUrl { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string SourceFilePath { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string DestinationFilePath { get; set; } = string.Empty;

    public MuseScoreViewModel(ILocalSettingsService localSettingsService)
    {
        Title = "MuseScore";
        _localSettingsService = localSettingsService;
    }


    [RelayCommand]
    private async Task DownloadAsync()
    {
        try
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            // TODO: Implement download logic

            // TODO: Implement extract logic

        }
        catch (Exception ex)
        {
            await _dialogService.ShowAlertDialogAsync(ex.Message);
        }
        finally
        {
            IsBusy = false;
            await ReportStatus("Ready downloading MuseScore");
        }
    }

}
