using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using SuperWinUtils.Contracts.ViewModels;
using SuperWinUtils.Core.Contracts.Services;
using SuperWinUtils.Core.Models;

namespace SuperWinUtils.ViewModels;

public partial class WaterMeterReaderViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    public ObservableCollection<WaterMeterReaderData> Source { get; } = [];

    public WaterMeterReaderViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        var data = await _sampleDataService.GetGridDataAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
