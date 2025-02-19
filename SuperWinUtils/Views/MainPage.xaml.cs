using Microsoft.UI.Xaml.Controls;

using SuperWinUtils.ViewModels;

namespace SuperWinUtils.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
