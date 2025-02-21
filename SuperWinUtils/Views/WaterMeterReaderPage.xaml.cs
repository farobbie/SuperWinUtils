using Microsoft.UI.Xaml.Controls;

using SuperWinUtils.ViewModels;

namespace SuperWinUtils.Views;

// TODO: Change the grid as appropriate for your app. Adjust the column definitions on DataGridPage.xaml.
// For more details, see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid.
public sealed partial class WaterMeterReaderPage : Page
{
    public WaterMeterReaderViewModel ViewModel
    {
        get;
    }

    public WaterMeterReaderPage()
    {
        ViewModel = App.GetService<WaterMeterReaderViewModel>();
        InitializeComponent();
    }
}
