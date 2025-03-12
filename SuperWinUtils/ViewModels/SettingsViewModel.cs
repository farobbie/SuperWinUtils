﻿using System.Reflection;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;

using SuperWinUtils.Contracts.Services;
using SuperWinUtils.Helpers;

using Windows.ApplicationModel;

namespace SuperWinUtils.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    private readonly IThemeSelectorService _themeSelectorService;

    [ObservableProperty]
    public partial ElementTheme ElementTheme { get; set; }

    [ObservableProperty]
    public partial string VersionDescription { get; set; }

    [ObservableProperty]
    public partial string VersionWSDK { get; set; }

    public SettingsViewModel(IThemeSelectorService themeSelectorService)
    {
        _themeSelectorService = themeSelectorService;
        ElementTheme = _themeSelectorService.Theme;
        VersionDescription = GetVersionDescription();
        VersionWSDK = GetAppVersion();
    }


    [RelayCommand]
    private async Task SwitchThemeAsync(ElementTheme param)
    {
        if (ElementTheme != param)
        {
            ElementTheme = param;
            await _themeSelectorService.SetThemeAsync(param);
        }
    }


    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }

    private string GetAppVersion()
    {
        return "";
        //var assembly = Assembly.Load("Microsoft.WindowsAppRuntime.Bootstrap");
        //var version = assembly?.GetName().Version;
        //return version != null ? "Version: " + version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision : "Unbekannt";
    }
}
