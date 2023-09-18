using YandexDisk.Client;

namespace TelegramBot.Console.YandexDisk.Services;

public interface IYandexDiskService
{
    ///<summary>
    /// Upload file to yandex disk
    ///</summary>
    Task<string?> UploadAsync(Stream stream, string fileName, string folder, CancellationToken cancellationToken = default(CancellationToken));

    ///<summary>
    /// Create folder if it is not exist
    ///</summary>
    Task<string?> CreateFolderAsync(string folderName, CancellationToken cancellationToken = default(CancellationToken));
}