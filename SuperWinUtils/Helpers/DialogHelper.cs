using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SuperWinUtils.Helpers;

public class DialogHelper
{
    private static ContentDialog CreateContentDialog(string title, object message, string primaryButtonText, string secondaryButtonText, string closeButtonText = "OK")
    {
        return new ContentDialog
        {
            XamlRoot = ((FrameworkElement)App.MainWindow.Content).XamlRoot,
            RequestedTheme = ((FrameworkElement)App.MainWindow.Content).RequestedTheme,
            Title = title,
            Content = message,
            PrimaryButtonText = primaryButtonText,
            SecondaryButtonText = secondaryButtonText,
            CloseButtonText = closeButtonText,

        };
    }

    public static async Task ShowAlertDialogAsync(string message)
    {
        var dialog = CreateContentDialog("Alert", message, string.Empty, string.Empty);
        await dialog.ShowAsync();
    }

    public static async Task ShowWarningDialogAsync(string message)
    {
        var dialog = CreateContentDialog("Warning", message, string.Empty, string.Empty);
        await dialog.ShowAsync();
    }

    public static async Task<ContentDialogResult> ShowOptionMessageDialogAsync(string title, string content, string primaryButtonText, string secondaryButtonText, string closeButtonText = "OK")
    {
        var dialog = CreateContentDialog(title, content, primaryButtonText, secondaryButtonText, closeButtonText);
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

        var dialog = CreateContentDialog(title, inputTextBox, primaryButtonText, secondaryButtonText, closeButtonText);
        return await dialog.ShowAsync();
    }
}
