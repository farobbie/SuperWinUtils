using CommunityToolkit.Mvvm.ComponentModel;

namespace SuperWinUtils.ViewModels;

public partial class BaseViewModel : ObservableRecipient
{
    public BaseViewModel()
    {

    }

    [ObservableProperty]
    public partial string? Title { get; set; }


    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    protected bool IsNotBusy => !IsBusy;
}
