using Telegram.Bot.Types;

namespace Telegram.Bot.States;

public interface IUserState
{
    Task Execute(Update update, CancellationToken cancellationToken);
}