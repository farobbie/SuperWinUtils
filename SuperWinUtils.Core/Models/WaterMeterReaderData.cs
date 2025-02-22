namespace SuperWinUtils.Core.Models;

public class WaterMeterReaderData
{
    public DateTime Date { get; set; }
    public int ColdWater { get; set; }
    public int WarmWater { get; set; }

    public int TotalWater => ColdWater + WarmWater;

    public int? ColdWaterDifference { get; set; } = null;
    public int? WarmWaterDifference { get; set; } = null;
}
