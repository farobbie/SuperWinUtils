using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperWinUtils.Contracts.Services;
using SuperWinUtils.Core.Contracts.Services;
using SuperWinUtils.Core.Models;
using SuperWinUtils.Helpers;
using SuperWinUtils.Models;
using static System.Net.WebRequestMethods;

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

    [ObservableProperty]
    public partial string SaveToolTip { get; set; }

    [ObservableProperty]
    public partial string RestoreToolTip { get; set; }


    [ObservableProperty]
    public partial DownloadState Download { get; set; }


    [ObservableProperty]
    public partial string VersionInstalled { get; set; }


    [ObservableProperty]
    public partial string VersionServer { get; set; }






    private readonly CancellationTokenSource _cancellationTokenSource;

    public MuseScoreViewModel(ILocalSettingsService localSettingsService, IFileExchangeService fileExchangeService, IArchiveService archiveService)
    {
        IsBusy = false;
        Title = "MuseScore";
        _localSettingsService = localSettingsService;
        _fileExchangeService = fileExchangeService;
        _archiveService = archiveService;

        SourceFileUrl = _defaultSourceFileUrl;
        SourceFilePath = _defaultSourceFilePath;
        DestinationFilePath = _defaultDestinationFilePath;
        _cancellationTokenSource = new();
        
        _ = InitializeAsync();

        SaveToolTip = "SettingEditorSaveTooltip".GetLocalized() ?? "Save";
        RestoreToolTip = "SettingEditorRestoreTooltip".GetLocalized() ?? "Restore";

        SourceFileUrl = @"https://ftp.osuosl.org/pub/musescore-nightlies/windows/4x/nightly/MuseScore-Studio-Nightly-latest-master-x86_64.7z";

        _ = GetInstalledVersionMuseScoreAsync();
        _ = ReportStatus("MuseScoreViewModel initialized");
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
        if (IsBusy)
        {
            return;
        }

        IsBusy = true;
        var success = true;
        try
        {
            

            // Validation

            if (string.IsNullOrWhiteSpace(SourceFileUrl))
            {
                await ReportStatus("SourceFileUrl is empty!");
                throw new ArgumentException("SourceFileUrl is not valid!", nameof(SourceFileUrl));
            }
            if (string.IsNullOrWhiteSpace(SourceFilePath))
            {
                await ReportStatus("SourceFilePath is empty!");
                throw new ArgumentException("SourceFilePath is not valid!", nameof(SourceFilePath));
            }
            if (string.IsNullOrWhiteSpace(DestinationFilePath))
            {
                await ReportStatus("DestinationFilePath is empty!");
                throw new ArgumentException("DestinationFilePath is not valid!", nameof(DestinationFilePath));
            }

            if(!Directory.Exists(SourceFilePath))
            {
                await ReportStatus("SourceFilePath does not exist!");
                throw new DirectoryNotFoundException("SourceFilePath does not exist!");
            }

            if (!Directory.Exists(DestinationFilePath))
            {
                await ReportStatus("DestinationFilePath does not exist!");
                throw new DirectoryNotFoundException("DestinationFilePath does not exist!");
            }

            // Check if file is newer

            var fileName = Path.GetFileName(new Uri(SourceFileUrl).AbsolutePath);
            var sourceFilePathX = Path.Combine(SourceFilePath, fileName);

            if (System.IO.File.Exists(sourceFilePathX))
            {
                var lastModified = await _fileExchangeService.GetFileDateAsync(SourceFileUrl);
                if (lastModified == null)
                {
                    await ReportStatus("Failed to get file date!");
                    throw new Exception("Failed to get file date!");
                }

                var fileDate = System.IO.File.GetLastWriteTime(sourceFilePathX);

                if (fileDate >= lastModified)
                {
                    await ReportStatus("File is up to date!");
                    return;
                }
                else
                {
                    await ReportStatus("File is not up to date!");
                    // Delete the file if it exists
                    try
                    {
                        System.IO.File.Delete(sourceFilePathX);
                    }
                    catch (Exception ex)
                    {
                        success = false;
                        await ReportStatus($"Failed to delete file: {ex.Message}");
                        throw;
                    }
                }
            }

            // Download MuseScore
            await DownloadMuseScoreFileAsync();

            await ReportStatus("Downloaded MuseScore");

            // Extract MuseScore
            //await ExtractMuseScoreFileAsync();

           //await ReportStatus("Extracted MuseScore");
        }
        catch (Exception ex)
        {
            success = false;
            await _dialogService.ShowAlertDialogAsync(ex.Message);
        }
        finally
        {
            IsBusy = false;
            if(success)
            {
                await ReportStatus("MuseScore download completed");
            }
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
        var progressDownload = new Progress<DownloadProgress>(info =>
        {
            Download.Progress = info.Progress;
            Download.ElapsedTime = info.ElapsedTime;
            Download.RemainingTime = info.RemainingTime;
            Download.Speed = info.Speed;
        });
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
    private async Task CheckVersionMuseScoreAsync()
    {
        await GetServerVersionMuseScoreAsync();
    }

    private async Task GetServerVersionMuseScoreAsync()
    {
        await ReportStatus("Get ServerVersion");
        var serverVersion = await _fileExchangeService.GetFileDateAsync(SourceFileUrl);


        VersionServer = serverVersion.Value.ToString("yyyy-MM-dd HH:mm:ss");
        Debug.WriteLine("VersionServer: " + VersionServer);
        await ReportStatus("Server: " + VersionServer);
    }

    private async Task GetInstalledVersionMuseScoreAsync()
    {
        var exePath = @"P:\MuseScore 4 Dev\bin\MuseScore4.exe";

        if(!System.IO.File.Exists(exePath))
        {
            await ReportStatus("MuseScore is not installed!");
            return;
        }


        var info = System.IO.File.GetCreationTime(exePath);
        VersionInstalled = info.ToString("yyyy-MM-dd HH:mm:ss");
    }



    [RelayCommand]
    private async Task SaveSettingAsync(string setting)
    {
        IsBusy = true;
        var (key, settingVar, message) = setting switch
        {
            "SourceFileUrl" => (_settingsKeySourceFileUrl, SourceFileUrl, "Save SourceFileUrl"),
            "SourceFilePath" => (_settingsKeySourceFilePath, SourceFilePath, "Save SourceFilePath"),
            "DestinationFilePath" => (_settingsKeyDestinationFilePath, DestinationFilePath, "Save DestinationFilePath"),
            _ => (string.Empty, string.Empty, "Wrong Textbox!")
        };

        if (key != null)
        {
            try
            {
                await _localSettingsService.SaveSettingAsync(key, settingVar);
            }
            catch
            {
                message = $"Failed to save setting {setting}!";
            }
        }

        await ReportStatus(message);
        IsBusy = false;
    }


    [RelayCommand]
    private async Task RestoreSettingAsync(string setting)
    {
        IsBusy = true;
        try
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
                    await ReportStatus($"Wrong Textbox {setting}!");
                    return;
            }
        }
        catch (Exception)
        {
            await ReportStatus($"Failed to restore setting {setting}!");
        }
        IsBusy = false;
    }


    [RelayCommand]
    private async Task PickFolderAsync(string button)
    {
        IsBusy = true;
        try
        {
            var folder = await base.PickFolderAsync();
            if(folder == null)
            {
                return;
            }
            switch (button)
            {
                case "SourceFilePath":
                    SourceFilePath = folder.Path;
                    await ReportStatus($"SourceFilePath: {SourceFilePath}");
                    break;
                case "DestinationFilePath":
                    DestinationFilePath = folder.Path;
                    break;
                default:
                    await ReportStatus($"Wrong Button {button}!");
                    return;

            }
        }
        catch (Exception ex)
        {
            await ReportStatus(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

}
