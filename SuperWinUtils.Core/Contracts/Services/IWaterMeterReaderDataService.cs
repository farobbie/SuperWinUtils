using SuperWinUtils.Core.Models;

namespace SuperWinUtils.Core.Contracts.Services;
public interface IWaterMeterReaderDataService
{
    Task<List<FileData>> ReadWaterMeterReaderDataAsync(List<FileData> fileDatas, IProgress<int> progress = null);
}
