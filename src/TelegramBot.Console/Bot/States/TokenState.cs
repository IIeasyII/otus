using Telegram.Bot.Types;
using TelegramBot.Console.YandexDisk.Services;
using YandexDisk.Client.Http;

namespace Telegram.Bot.States;

public class TokenState : IUserState
{
    private readonly ITelegramBotClient _botClient;

    public TokenState(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task Execute(Update update, CancellationToken cancellationToken = default(CancellationToken))
    {
        var message = update.Message;
        if(message is null) return;

        var user = message.From;
        if(user is null) return;

        var token = message.Text;
        if(token is null) return;

        if(!YandexDiskBot.Tokens.TryAdd(user.Id, token)) return;

        var chatId = message.Chat.Id;

        await _botClient.SendTextMessageAsync(chatId, @$"Сейчас создам папку по умолчанию, в которую буду сохранять файлы.", cancellationToken: cancellationToken);

        var diskApi = new DiskHttpApi(token);
        var yandexDiskService = new YandexDiskService(diskApi);

        await yandexDiskService.CreateDefaultFolderAsync();

        await _botClient.SendTextMessageAsync(chatId, @$"Теперь можешь скидывать файлы, а я буду их сохранять:)", cancellationToken: cancellationToken);

        YandexDiskBot.States[user.Id] = new UploadsState(_botClient);
    }
}