using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types;
using TelegramBot;

namespace Telegram.Bot.States;

public class WelcomeState : IUserState
{
    private readonly ITelegramBotClient _botClient;
    private readonly string _yandexUrl;

    public WelcomeState(ITelegramBotClient botClient, IConfiguration configuration)
    {
        _botClient = botClient;
        _yandexUrl = configuration[EnvironmentVariables.YANDEX_DISK_TOKEN_URL]!;
    }

    public async Task Execute(Update update, CancellationToken cancellationToken = default(CancellationToken))
    {
        var message = update.Message;
        if(message is null) return;

        var chatId = message.Chat.Id;
        await _botClient.SendTextMessageAsync(chatId, $"Привет, я Otus бот, который загрузит твои файлы к тебе на Яндекс.Диск! Перейди пожалуйста по ссылке и отправь мне токен доступа к твоему Яндекс.Диску: \n{_yandexUrl}");

        var user = message.From!;
        YandexDiskBot.States[user.Id] = new TokenState(_botClient);
    }
}