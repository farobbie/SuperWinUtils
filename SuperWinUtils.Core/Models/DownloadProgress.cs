namespace SuperWinUtils.Core.Models;
public class DownloadProgress
{
    public long TotalBytes { get; set; }
    public long BytesReceived { get; set; }
    public double Progress => TotalBytes == 0 ? 0 : (double)BytesReceived / TotalBytes;
    public TimeSpan ElapsedTime { get; set; }
    public TimeSpan RemainingTime { get; set; }
    public int Speed { get; set; }
}
