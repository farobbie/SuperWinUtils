using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.UI.Xaml.Media.Imaging;
using SuperWinUtils.Core.Models;
using Windows.Storage;
using Windows.Storage.Streams;

namespace SuperWinUtils.Helpers;
public class ImageConverter
{
    private static readonly HashSet<string> ImageMimeTypes =
    [
        "image/jpeg", "image/png", "image/gif", "image/bmp", "image/tiff", "image/webp", "image/svg+xml"
    ];

    private static async Task<bool> IsImageAsync(StorageFile file)
    {
        if (file == null)
        {
            return false;
        }

        var properties = await file.Properties.RetrievePropertiesAsync(["System.ContentType"]);
        if (properties.TryGetValue("System.ContentType", out var value) && value is string mimeType)
        {
            return ImageMimeTypes.Contains(mimeType);
        }
        return false;
    }

    private static async Task<byte[]> ReadImageAsBytesAsync(StorageFile file)
    {
        if (file == null)
        {
            return [];
        }

        using IRandomAccessStream stream = await file.OpenReadAsync();
        using MemoryStream ms = new();
        await stream.AsStreamForRead().CopyToAsync(ms);
        return ms.ToArray();
    }

    public static async Task<FileData?> ConvertToFileDataAsync(StorageFile file)
    {
        if (!(await IsImageAsync(file)))
        {
            return null;
        }

        return new FileData
        {
            Name = file.Name,
            Path = file.Path,
            Date = file.DateCreated.DateTime,
            Content = await ReadImageAsBytesAsync(file)
        };
    }

    public static async Task<BitmapImage> BitmapToBitmapImageAsync(Bitmap bitmap)
    {
        using MemoryStream memoryStream = new();
        bitmap.Save(memoryStream, ImageFormat.Png);
        memoryStream.Seek(0, SeekOrigin.Begin);

        BitmapImage bitmapImage = new();
        using var stream = memoryStream.AsRandomAccessStream();
        await bitmapImage.SetSourceAsync(stream);
        return bitmapImage;
    }
}