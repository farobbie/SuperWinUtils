using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperWinUtils.Helpers;

namespace SuperWinUtils.ViewModels;

public partial class MuseScoreViewModel : BaseViewModel
{
    public MuseScoreViewModel()
    {
        Title = "MuseScore";
    }


    [RelayCommand]
    private async Task DownloadAsync()
    {
        try
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

        }
        catch (Exception ex)
        {
            await DialogHelper.ShowAlertDialogAsync($"{ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

}
