using Microsoft.UI.Xaml.Controls;

namespace SuperWinUtils.Contracts.Services;

public interface IDialogService
{
    Task ShowAlertDialogAsync(string message);
    Task ShowWarningDialogAsync(string message);
    Task<ContentDialogResult> ShowOptionMessageDialogAsync(string title, string content, string primaryButtonText, string secondaryButtonText, string closeButtonText = "OK");
    Task<ContentDialogResult> InputStringDialogAsync(string title, string defaultText, string primaryButtonText, string secondaryButtonText, string closeButtonText = "Cancel");
}
