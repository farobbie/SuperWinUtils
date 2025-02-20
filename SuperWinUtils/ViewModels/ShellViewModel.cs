using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Navigation;

using SuperWinUtils.Contracts.Services;
using SuperWinUtils.Views;

namespace SuperWinUtils.ViewModels;

public partial class ShellViewModel : BaseViewModel
{

    [ObservableProperty]
    public partial bool IsBackEnabled { get; set; }


    [ObservableProperty]
    public partial object? Selected { get; set; }


    [ObservableProperty]
    public partial string StatusMessage { get; set; }


    public INavigationService NavigationService { get; }

    public INavigationViewService NavigationViewService { get; }

    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService)
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;

        StatusMessage = string.Empty;
        _statusService.StatusProgress = new Progress<string>(msg => StatusMessage = msg);
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage))
        {
            Selected = NavigationViewService.SettingsItem;
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null)
        {
            Selected = selectedItem;
        }
    }
}
