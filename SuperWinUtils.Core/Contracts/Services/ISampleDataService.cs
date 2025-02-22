using SuperWinUtils.Core.Models;

namespace SuperWinUtils.Core.Contracts.Services;

// Remove this class once your pages/features are using your data.
public interface ISampleDataService
{
    Task<IEnumerable<WaterMeterReaderData>> GetGridDataAsync();
}
