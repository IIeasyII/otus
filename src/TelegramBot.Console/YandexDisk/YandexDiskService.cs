using YandexDisk.Client;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Protocol;

namespace TelegramBot.Console.YandexDisk.Services;

public class YandexDiskService : IYandexDiskService
{
    private readonly IDiskApi _diskApi;
    private const string DiskRoot = "/";

    public YandexDiskService(IDiskApi diskApi)
    {
        _diskApi = diskApi;
    }

    public async Task<string?> UploadAsync(Stream stream, string fileName, string? folderName, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var fullName = $"{folderName}/{fileName}";
            await _diskApi.Files.UploadFileAsync(fullName, false, stream, cancellationToken);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.ToString());
            return $"Файл '{fileName}' не получилось загрузить:( Возможно он уже есть на диске?";
        }

        return null;
    }

    public async Task<string?> CreateFolderAsync(string folderName, CancellationToken cancellationToken = default)
    {
        var diskInfo = await _diskApi.MetaInfo.GetInfoAsync(new ResourceRequest { Path = DiskRoot });
        if (diskInfo is null) return "Что-то пошло не так...";

        var folderExist = diskInfo.Embedded.Items.Any(x => x.Name == folderName && x.MimeType == null);
        if (folderExist) return "Не могу найти папку, которую ты указал.";

        await _diskApi.Commands.CreateDictionaryAsync(folderName, cancellationToken);
        return null;
    }
}