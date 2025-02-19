using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SuperWinUtils.Helpers;

public class DialogHelper
{
    private static async Task ShowMessageDialogAsync(string title, object message, string primaryButtonText, string secondaryButtonText, string closeButtonText = "OK")
    {
        var dialog = new ContentDialog
        {
            XamlRoot = ((FrameworkElement)App.MainWindow.Content).XamlRoot,
            RequestedTheme = ((FrameworkElement)App.MainWindow.Content).RequestedTheme,
            Title = title,
            Content = message,
            PrimaryButtonText = primaryButtonText,
            SecondaryButtonText = secondaryButtonText,
            CloseButtonText = closeButtonText,

        };
        await dialog.ShowAsync();

    }

    public static async Task ShowAlertDialogAsync(string message)
    {
        await ShowMessageDialogAsync("Error", message, string.Empty, string.Empty);
    }

    public static async Task ShowWarningDialogAsync(string message)
    {
        await ShowMessageDialogAsync("Warning", message, string.Empty, string.Empty);
    }

    public static async Task<ContentDialogResult> ShowOptionMessageDialogAsync(string title, string content, string primaryButtonText, string secondaryButtonText, string closeButtonText = "OK")
    {
        var dialog = new ContentDialog
        {
            XamlRoot = ((FrameworkElement)App.MainWindow.Content).XamlRoot,
            RequestedTheme = ((FrameworkElement)App.MainWindow.Content).RequestedTheme,
            Title = title,
            Content = content,
            PrimaryButtonText = primaryButtonText,
            SecondaryButtonText = secondaryButtonText,
            CloseButtonText = closeButtonText,
        };
        return await dialog.ShowAsync();
    }

    public static async Task<ContentDialogResult> InputStringDialogAsync(string title, string defaultText, string primaryButtonText, string secondaryButtonText, string closeButtonText = "Cancel")
    {
        var inputTextBox = new TextBox
        {
            XamlRoot = App.MainWindow.Content.XamlRoot,
            AcceptsReturn = false,
            Height = 32,
            Text = defaultText,
            SelectionStart = defaultText.Length,
        };

        var dialog = new ContentDialog
        {
            XamlRoot = ((FrameworkElement)App.MainWindow.Content).XamlRoot,
            RequestedTheme = ((FrameworkElement)App.MainWindow.Content).RequestedTheme,
            Title = title,
            Content = inputTextBox,
            PrimaryButtonText = primaryButtonText,
            SecondaryButtonText = secondaryButtonText,
            CloseButtonText = closeButtonText,
        };
        return await dialog.ShowAsync();
    }
}
