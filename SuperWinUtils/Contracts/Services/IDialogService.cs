﻿using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage;

namespace SuperWinUtils.Contracts.Services;

public interface IDialogService
{
    Task ShowImageWithText(string message, BitmapImage image);
    Task ShowAlertDialogAsync(string message);
    Task ShowWarningDialogAsync(string message);
    Task<ContentDialogResult> ShowOptionMessageDialogAsync(string title, string content, string primaryButtonText, string secondaryButtonText, string closeButtonText = "OK");
    Task<ContentDialogResult> InputStringDialogAsync(string title, string defaultText, string primaryButtonText, string secondaryButtonText, string closeButtonText = "Cancel");
    Task<IReadOnlyList<StorageFile>> OpenImagesAsync();
    Task<StorageFolder> PickFolderAsync();
}
