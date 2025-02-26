using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperWinUtils.Contracts.Services;
using SuperWinUtils.Core.Contracts.Services;
using SuperWinUtils.Core.Models;
using SuperWinUtils.Helpers;

namespace SuperWinUtils.ViewModels;

public partial class MuseScoreViewModel : BaseViewModel
{
    private readonly ILocalSettingsService _localSettingsService;
    private readonly IFileExchangeService _fileExchangeService;
    private readonly IArchiveService _archiveService;

    private const string _settingsKeySourceFileUrl = "MuseScore_SourceFileUrl";
    private const string _settingsKeySourceFilePath = "MuseScore_SourceFilePath";
    private const string _settingsKeyDestinationFilePath = "MuseScore_DestinationFilePath";

    private readonly string _defaultSourceFileUrl = "MuseScore_SourceFileUrl_Default".GetLocalized() ?? "SourceFileUrl";
    private readonly string _defaultSourceFilePath = "MuseScore_SourceFilePath_Default".GetLocalized() ?? "SourceFilePath";
    private readonly string _defaultDestinationFilePath = "MuseScore_DestinationFilePath_Default".GetLocalized() ?? "DestinationFilePath";

    [ObservableProperty]
    public partial string SourceFileUrl { get; set; }

    [ObservableProperty]
    public partial string SourceFilePath { get; set; }

    [ObservableProperty]
    public partial string DestinationFilePath { get; set; }

    private readonly CancellationTokenSource _cancellationTokenSource;

    public MuseScoreViewModel(ILocalSettingsService localSettingsService, IFileExchangeService fileExchangeService, IArchiveService archiveService)
    {
        Title = "MuseScore";
        _localSettingsService = localSettingsService;
        _fileExchangeService = fileExchangeService;
        _archiveService = archiveService;

        SourceFileUrl = _defaultSourceFileUrl;
        SourceFilePath = _defaultSourceFilePath;
        DestinationFilePath = _defaultDestinationFilePath;
        _cancellationTokenSource = new();
        
        _ = InitializeAsync();
    }

    public async Task InitializeAsync()
    {
        try
        {
            SourceFileUrl = await _localSettingsService.ReadSettingAsync<string>(_settingsKeySourceFileUrl) ?? _defaultSourceFileUrl;
            SourceFilePath = await _localSettingsService.ReadSettingAsync<string>(_settingsKeySourceFilePath) ?? _defaultSourceFilePath;
            DestinationFilePath = await _localSettingsService.ReadSettingAsync<string>(_settingsKeyDestinationFilePath) ?? _defaultDestinationFilePath;
        }
        catch (Exception ex)
        {
            await _dialogService.ShowAlertDialogAsync(ex.Message);
        }
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

            // Download MuseScore
            await DownloadMuseScoreFileAsync();

            await ReportStatus("Downloaded MuseScore");

            // Extract MuseScore
            await ExtractMuseScoreFileAsync();

            await ReportStatus("Extracted MuseScore");
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

    private async Task ExtractMuseScoreFileAsync()
    {
        var progressArchive = new Progress<double>();
        progressArchive.ProgressChanged += async (_, data) =>
        {
            await ReportStatus($"Extracting MuseScore: {data}%");
        };

        await _archiveService.ExtractFileToAsync(SourceFilePath, DestinationFilePath, progressArchive, _cancellationTokenSource.Token);

    }

    private async Task DownloadMuseScoreFileAsync()
    {
        var progressDownload = new Progress<DownloadProgress>();
        progressDownload.ProgressChanged += async (_, data) =>
        {
            var progressPercentage = data.Progress * 100;
            await ReportStatus($"Downloading MuseScore: {progressPercentage}%");
        };

        await _fileExchangeService.DownloadFileAsync(SourceFileUrl, SourceFilePath, progressDownload, _cancellationTokenSource.Token);
    }

    [RelayCommand]
    private async Task CancelDownloadAsync()
    {
        await _cancellationTokenSource.CancelAsync();
    }


    [RelayCommand]
    private async Task SaveSettingAsync(string setting)
    {
        var (key, settingVar, message) = setting switch
        {
            "SourceFileUrl" => (_settingsKeySourceFileUrl, SourceFileUrl, "Save SourceFileUrl"),
            "SourceFilePath" => (_settingsKeySourceFilePath, SourceFilePath, "Save SourceFilePath"),
            "DestinationFilePath" => (_settingsKeyDestinationFilePath, DestinationFilePath, "Save DestinationFilePath"),
            _ => (string.Empty, string.Empty, "Wrong Textbox!")
        };

        if (key != null)
        {
            await _localSettingsService.SaveSettingAsync(key, settingVar);
        }

        await ReportStatus(message);
    }


    [RelayCommand]
    private async Task ResetSettingAsync(string setting)
    {
        switch (setting)
        {
            case "SourceFileUrl":
                SourceFileUrl = _defaultSourceFileUrl;
                await _localSettingsService.SaveSettingAsync(_settingsKeySourceFileUrl, SourceFileUrl);
                break;
            case "SourceFilePath":
                SourceFilePath = _defaultSourceFilePath;
                await _localSettingsService.SaveSettingAsync(_settingsKeySourceFilePath, SourceFilePath);
                break;
            case "DestinationFilePath":
                DestinationFilePath = _defaultDestinationFilePath;
                await _localSettingsService.SaveSettingAsync(_settingsKeyDestinationFilePath, DestinationFilePath);
                break;
            default:
                await ReportStatus($"Wrong Textbox! {setting}");
                return;
        }
    }
}
