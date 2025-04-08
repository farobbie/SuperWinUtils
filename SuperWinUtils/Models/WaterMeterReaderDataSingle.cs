using CommunityToolkit.Mvvm.ComponentModel;

namespace SuperWinUtils.Models;
public partial class WaterMeterReaderDataSingle : ObservableValidator
{

    [ObservableProperty]
    public partial int WarmWater { get; set; }


    [ObservableProperty]
    public partial int ColdWater { get; set; }


    [ObservableProperty]
    public partial DateTime Date { get; set; }
}
