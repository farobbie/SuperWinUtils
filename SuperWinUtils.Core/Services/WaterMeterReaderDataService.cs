using System.Drawing;
using System.Text;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.OCR;
using Emgu.CV.Util;
using SuperWinUtils.Core.Contracts.Services;
using SuperWinUtils.Core.Models;

namespace SuperWinUtils.Core.Services;
public class WaterMeterReaderDataService : IWaterMeterReaderDataService
{
    private readonly List<FileData> _fileDatas;
    private readonly Tesseract _tesseract;
    public WaterMeterReaderDataService()
    {
        _fileDatas = [];
        var currentPath = Path.Combine(AppContext.BaseDirectory, "Tessdata");

        _tesseract = new Tesseract(currentPath, "eng", OcrEngineMode.TesseractLstmCombined, "0123456789")
        {
            
        };
    }

    public Task<List<FileData>> ReadWaterMeterReaderDataAsync(List<FileData> fileDatas, IProgress<int> progress = null) 
        => throw new NotImplementedException();
}
