using Microsoft.Extensions.Configuration;
using Telegram.Bot.States;
using Telegram.Bot.Types;
using TelegramBot;

namespace Telegram.Bot;

public class YandexDiskBot
{
    private readonly TelegramBotClient _botClient;
    private readonly IConfiguration _configuration;

    public YandexDiskBot(IConfiguration configuration)
    {
        var token = configuration[EnvironmentVariables.TELEGRAM_BOT_TOKEN]!;
        _botClient = new TelegramBotClient(token);
        _configuration = configuration;
    }

    public async Task StartAsync()
    {
        await _botClient.ReceiveAsync(UpdateHandler, PollingErrorHandler);
    }

    internal static readonly Dictionary<long, string> Tokens = new();
    internal static readonly Dictionary<long, IUserState> States = new();
    internal static readonly Dictionary<User, long> Chats = new();

    private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        if (message is null) return;

        var user = message.From;
        if (user is null) return;

        var state = States.ContainsKey(user.Id) ? States[user.Id] : new WelcomeState(botClient, _configuration);

        await state.Execute(update, cancellationToken);
    }

    private async Task PollingErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {

    }
}