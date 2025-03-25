using SuperWinUtils.Core.Models;
using Windows.Storage;
using Windows.Storage.Streams;

namespace SuperWinUtils.Helpers;
public class ToFileDataConverter
{
    private static readonly HashSet<string> ImageMimeTypes =
    [
        "image/jpeg", "image/png", "image/gif", "image/bmp", "image/tiff", "image/webp", "image/svg+xml"
    ];

    private static async Task<bool> IsImageAsync(StorageFile file)
    {
        if (file == null)
            return false;

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
            return [];
        var buffer = await FileIO.ReadBufferAsync(file);
        using var reader = DataReader.FromBuffer(buffer);
        var bytes = new byte[buffer.Length];
        reader.ReadBytes(bytes);
        return bytes;
    }

    public static async Task<FileData> ConvertToFileDataAsync(StorageFile file)
    {
        if (await IsImageAsync(file))
        {
            throw new ArgumentException("ExceptionToFileDataConverterFileTypeNotSupported");
        }

        return new FileData
        {
            Name = file.Name,
            Path = file.Path,
            Date = file.DateCreated.DateTime,
            Content = await ReadImageAsBytesAsync(file)
        };
    }
}