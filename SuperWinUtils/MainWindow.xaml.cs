using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SuperWinUtils.Helpers;

using Windows.UI.ViewManagement;

namespace SuperWinUtils;

public sealed partial class MainWindow : WindowEx
{
    private readonly Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;

    private readonly UISettings settings;

    public MainWindow()
    {
        InitializeComponent();

        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/WindowIcon.ico"));
        Content = null;
        Title = "AppDisplayName".GetLocalized();

        // Theme change code picked from https://github.com/microsoft/WinUI-Gallery/pull/1239
        dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        settings = new UISettings();
        settings.ColorValuesChanged += Settings_ColorValuesChanged; // cannot use FrameworkElement.ActualThemeChanged event
        Closed += MainWindow_Closed;
    }

    // this handles updating the caption button colors correctly when indows system theme is changed
    // while the app is open
    private void Settings_ColorValuesChanged(UISettings sender, object args)
    {
        // This calls comes off-thread, hence we will need to dispatch it to current app's thread
        dispatcherQueue.TryEnqueue(() =>
        {
            TitleBarHelper.ApplySystemThemeToCaptionButtons();
        });
    }

    private async void MainWindow_Closed(object sender, WindowEventArgs e)
    {
        var dialog = new ContentDialog
        {
            Title = "Confirmation",
            Content = "Do you really want to close this app?",
            PrimaryButtonText = "Yes",
            SecondaryButtonText = "Cancel",
            XamlRoot = App.MainWindow.Content.XamlRoot
        };

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Secondary)
        {
            e.Handled = true;
        }
        else
        {

        }
    }
}
