﻿using CommunityToolkit.Mvvm.ComponentModel;
using SuperWinUtils.Contracts.Services;

namespace SuperWinUtils.ViewModels;

public partial class BaseViewModel : ObservableRecipient
{
    protected readonly IStatusService _statusService = App.GetService<IStatusService>();

    [ObservableProperty]
    public partial string? Title { get; set; }

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    protected bool IsNotBusy => !IsBusy;

    public BaseViewModel()
    {
        
    }

    protected async Task UpdateStatus(string statusMessage)
    {
        await _statusService.UpdateMessage($"{Title}: {statusMessage}");
    }
}
