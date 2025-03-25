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
        _tesseract = new Tesseract("./tessdata", "eng", OcrEngineMode.TesseractOnly, "IlioO01223456789")
        {
            PageSegMode = PageSegMode.SingleLine
        };
    }

    public async Task ReadWaterMeterReaderDataAsync(List<FileData> fileDatas)
    {
        _fileDatas.AddRange(fileDatas);
        await Parallel.ForEachAsync(_fileDatas, async (file, _) =>
        {
            file.Pix = await ConvertByteToPix(file.Content);
            file.Text = await ReadWaterMeterReaderDataFromPixAsync(file.Pix);
        });
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
