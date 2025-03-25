using Emgu.CV.OCR;

namespace SuperWinUtils.Core.Models;
public class FileData
{
    public string Name { get; set; }
    public string Path { get; set; }
    public DateTime Date { get; set; }
    public byte[] Content { get; set; }
    public Pix Pix { get; set; }
    public string Text { get; set; }

}