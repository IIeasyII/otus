namespace TelegramBot.Console.YandexDisk.OAuth;

public interface IYandexOAuthClient
{
    Task<string> GetOAuthUserToken();
}