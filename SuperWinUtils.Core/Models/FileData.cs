using Emgu.CV;

namespace SuperWinUtils.Core.Models;
public class FileData
{
    public string Name { get; set; }
    public string Path { get; set; }
    public DateTime Date { get; set; }
    public byte[] Content { get; set; }
    public string FileFormat { get; set; }

    public Mat Mat { get; set; }
    public Mat GrayMat { get; set; }

    public string Text { get; set; }

}