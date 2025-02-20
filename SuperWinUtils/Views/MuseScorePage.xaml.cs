using Microsoft.UI.Xaml.Controls;

using SuperWinUtils.ViewModels;

namespace SuperWinUtils.Views;

public sealed partial class MuseScorePage : Page
{
    public MuseScoreViewModel ViewModel
    {
        get;
    }

    public MuseScorePage()
    {
        ViewModel = App.GetService<MuseScoreViewModel>();
        InitializeComponent();
    }
}
