using Telegram.Bot.Types;
using TelegramBot.Console.YandexDisk.Services;
using YandexDisk.Client.Http;

namespace Telegram.Bot.States;

public class UploadsState : IUserState
{
    private readonly ITelegramBotClient _botClient;

    public UploadsState(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task Execute(Update update, CancellationToken cancellationToken = default(CancellationToken))
    {
        var message = update.Message;
        if (message is null) return;

        var user = message.From;
        if (user is null) return;

        var token = YandexDiskBot.Tokens[user.Id];
        var diskApi = new DiskHttpApi(token);
        var yandexDiskService = new YandexDiskService(diskApi);

        //Если к загружаемым файлам добавить 'приписку', то создаем папку
        var caption = message.Caption;
        if (caption is not null)
        {
            await yandexDiskService.CreateFolderAsync(caption, cancellationToken);
        }

        var chatId = message.Chat.Id;

        var document = message.Document;
        if (document is null)
        {
            await _botClient.SendTextMessageAsync(chatId, "Не очень это похоже на файл...");
            return;
        }

        var fileId = document.FileId;
        var fileName = document.FileName;
        if (fileName is null)
        {
            await _botClient.SendTextMessageAsync(chatId, "Дай, пожалуйста, название файлу, а то не сохраню");
            return;
        }

        var file = await _botClient.GetFileAsync(fileId, cancellationToken);
        if (file is null)
        {
            await _botClient.SendTextMessageAsync(chatId, "Упс, телеграм не отдает мне твой файл:|");
            return;
        }

        using var stream = new MemoryStream();
        await _botClient.DownloadFileAsync(file.FilePath!, stream);

        stream.Seek(0, SeekOrigin.Begin);
        var error = await yandexDiskService.UploadAsync(stream, fileName, caption, cancellationToken);
        if (error is not null)
        {
            await _botClient.SendTextMessageAsync(chatId, error);
        }
    }
}