using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperWinUtils.Contracts.ViewModels;
using SuperWinUtils.Core.Contracts.Services;
using SuperWinUtils.Core.Models;

namespace SuperWinUtils.ViewModels;

public partial class WaterMeterReaderViewModel : BaseViewModel, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;
    private readonly IWaterMeterReaderDataService _waterMeterReaderDataService;


    [ObservableProperty]
    public partial int WarmWaterSingleData { get; set; }


    [ObservableProperty]
    public partial int ColdWaterSingleData { get; set; }


    [ObservableProperty]
    public partial DateTimeOffset DateSingleData { get; set; }


    [ObservableProperty]
    public partial TimeSpan TimeSingleData { get; set; }





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


    [RelayCommand]
    public async Task AddSingleDataAsync()
    {
        if (IsBusy)
        {
            return;
        }

        var data = new WaterMeterReaderData
        {
            WarmWater = WarmWaterSingleData,
            ColdWater = ColdWaterSingleData,
            Date = new DateTime(DateSingleData.Year, DateSingleData.Month, DateSingleData.Day, TimeSingleData.Hours, TimeSingleData.Minutes, 0)
        };


        if (data.WarmWater < 0 || data.ColdWater < 0)
        {
            await ShowAlert("Invalid data");
            return;
        }

        if (Source.Any(d => d.Date.Date == data.Date.Date))
        {
            await ShowAlert("Data for this date already exists.");
            return;
        }

        await AddToSource(data);
    }

    partial void OnWarmWaterSingleDataChanged(int value)
    {
    }

    public async void OnNavigatedTo(object parameter)
    {
        await GetSampleData();
    }

    public void OnNavigatedFrom()
    {
    }
}
