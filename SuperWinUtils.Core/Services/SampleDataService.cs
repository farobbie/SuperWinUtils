using SuperWinUtils.Core.Contracts.Services;
using SuperWinUtils.Core.Models;

namespace SuperWinUtils.Core.Services;

public class SampleDataService : ISampleDataService
{
    private List<WaterMeterReaderData> _allWaterMeterDatas;

    public SampleDataService()
    {
    }

    private static IEnumerable<WaterMeterReaderData> AllWaterMeterDatas()
    {
        IEnumerable<WaterMeterReaderData> datas = 
        [
            new()
            { Date = new DateTime(2025, 1, 1, 12, 14, 0), ColdWater = 1000, WarmWater = 200 },
            new()
            { Date = new DateTime(2025, 1, 2, 10, 0, 0), ColdWater = 1125, WarmWater = 225 },
            new()
            { Date = new DateTime(2025, 1, 3, 10, 15, 0), ColdWater = 1258, WarmWater = 250 },
            new()
            { Date = new DateTime(2025, 1, 4, 10, 15, 0), ColdWater = 1326, WarmWater = 266 },
        ];

        // Calculate differences
        foreach (var (previous, current) in datas.Zip(datas.Skip(1)))
        {
            current.ColdWaterDifference = current.ColdWater - previous.ColdWater;
            current.WarmWaterDifference = current.WarmWater - previous.WarmWater;
        }

        return datas;
    }

    public Task<IEnumerable<WaterMeterReaderData>> GetGridDataAsync()
    {
        _allWaterMeterDatas ??= [.. AllWaterMeterDatas()];

        return Task.FromResult<IEnumerable<WaterMeterReaderData>>(_allWaterMeterDatas);
    }
}
