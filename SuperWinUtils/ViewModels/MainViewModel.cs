namespace SuperWinUtils.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public MainViewModel()
    {
        Title = "Main";
        _ = UpdateStatus($"Constructed + {DateTime.Now}");
    }
}
