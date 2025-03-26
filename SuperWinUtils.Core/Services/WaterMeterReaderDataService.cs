using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.OCR;
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
        
        _tesseract = new Tesseract(currentPath, "eng", OcrEngineMode.TesseractOnly, "IlioO01223456789")
        {
            PageSegMode = PageSegMode.SingleLine
        };
    }

    public async Task<List<FileData>> ReadWaterMeterReaderDataAsync(List<FileData> fileDatas, IProgress<int> progress = null)
    {
        _fileDatas.AddRange(fileDatas);
        var total = _fileDatas.Count;
        var processed = 0;

        var task = _fileDatas.Select(async file =>
        {
            file.Pix = await ConvertByteToPix(file.Content);
            file.Text = await ReadWaterMeterReaderDataFromPixAsync(file.Pix);

            var currentProgress = Interlocked.Increment(ref processed);
            progress?.Report(currentProgress * 100 / total);
        });

        await Task.WhenAll(task);

        return _fileDatas;
    }

    private static async Task<Pix> ConvertByteToPix(byte[] content)
    {
        await Task.CompletedTask;
        using var dst = new Mat();
        CvInvoke.Imdecode(content, ImreadModes.Color, dst);
        return new Pix(dst);
    }

    private async Task<string> ReadWaterMeterReaderDataFromPixAsync(Pix dataPix)
    {
        await Task.CompletedTask;

        var tesseract = _tesseract;
        //set an image for the engine
        tesseract.SetImage(dataPix);
        //recognize the text
        tesseract.Recognize();
        //get the text
        var data = tesseract.GetOsdText();

        return data;
    }
}
