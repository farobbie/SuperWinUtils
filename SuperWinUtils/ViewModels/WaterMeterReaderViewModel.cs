﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using SuperWinUtils.Contracts.ViewModels;
using SuperWinUtils.Core.Contracts.Services;
using SuperWinUtils.Core.Models;

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
    private async Task LoadWaterMeterReaderData()
    {
        try
        {
            if(IsBusy)
            {
                return;
            }

            IsBusy = true;

            // open files for reading
            var files = await LoadImagesAsync();

            // TODO: convert files to fileData

            // TODO: send files to service


        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            IsBusy = false;
        }
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
