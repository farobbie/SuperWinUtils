using CommunityToolkit.Mvvm.ComponentModel;

namespace SuperWinUtils.Models;
public partial class DownloadState : ObservableObject
{

    [ObservableProperty]
    public partial double Progress { get; set; }

    [ObservableProperty]
    public partial bool IsDownloading { get; set; }

    [ObservableProperty]
    public partial TimeSpan ElapsedTime { get; set; }

    [ObservableProperty]
    public partial TimeSpan RemainingTime { get; set; }

    [ObservableProperty]
    public partial double Speed { get; set; }
}
