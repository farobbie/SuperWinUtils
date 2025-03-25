using SuperWinUtils.Core.Models;

namespace SuperWinUtils.Core.Contracts.Services;
public interface IWaterMeterReaderDataService
{
    Task ReadWaterMeterReaderDataAsync(List<FileData> fileDatas);
}
