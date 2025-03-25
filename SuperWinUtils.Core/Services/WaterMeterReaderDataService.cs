using Emgu.CV.OCR;
using SuperWinUtils.Core.Contracts.Services;

namespace SuperWinUtils.Core.Services;
public class WaterMeterReaderDataService : IWaterMeterReaderDataService
{
    public WaterMeterReaderDataService()
    {

    }

    //private async Task<string> ReadWaterMeterData(Pix dataPix)
    //{
    //    await Task.CompletedTask;
    //    var tesseract = new Tesseract("./tessdata", "eng", OcrEngineMode.TesseractOnly, "IlioO01223456789")
    //    {
    //        PageSegMode = PageSegMode.SingleLine
    //    };


    //    //set an image for the engine
    //    // tesseract.SetImage();
    //    //recognize the text
    //    tesseract.Recognize();
    //    //get the text
    //    var data = tesseract.GetOsdText();

    //    return data;
    //}

}
