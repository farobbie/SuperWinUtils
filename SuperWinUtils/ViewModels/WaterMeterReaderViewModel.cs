using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using Emgu.CV;
using SuperWinUtils.Contracts.ViewModels;
using SuperWinUtils.Core.Contracts.Services;
using SuperWinUtils.Core.Models;
using SuperWinUtils.Helpers;
using Windows.Networking.NetworkOperators;

namespace SuperWinUtils.ViewModels;

public partial class WaterMeterReaderViewModel : BaseViewModel, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;
    private readonly IWaterMeterReaderDataService _waterMeterReaderDataService;

    public ObservableCollection<WaterMeterReaderData> Source { get; } = [];

    public WaterMeterReaderViewModel(ISampleDataService sampleDataService, IWaterMeterReaderDataService waterMeterReaderDataService)
    {
        _sampleDataService = sampleDataService;
        _waterMeterReaderDataService = waterMeterReaderDataService;
    }


    [RelayCommand]
    public async Task AddWaterMeterReaderDataAsync()
    {
        try
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            var files = await LoadImagesAsync();

        }
        catch (Exception e)
        {
            await ShowAlert($"Error: {e.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task AddToSource(WaterMeterReaderData data)
    {
        await Task.CompletedTask;
        Source.Add(data);
    }

    private async Task GetSampleData()
    {
        Source.Clear();

        var data = await _sampleDataService.GetGridDataAsync();

        foreach (var item in data)
        {
            await AddToSource(item);
        }
    }


    public async void OnNavigatedTo(object parameter)
    {
        await GetSampleData();
    }

    public void OnNavigatedFrom()
    {
    }
}
