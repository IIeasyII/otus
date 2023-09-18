using Microsoft.Extensions.Configuration;

namespace TelegramBot.Extensions;

public static class ConfigurationExtension
{
    public static void Validate(this IConfiguration configuration)
    {
        var botToken = !string.IsNullOrWhiteSpace(configuration[EnvironmentVariables.TELEGRAM_BOT_TOKEN]);
        var yandexUrl = !string.IsNullOrWhiteSpace(configuration[EnvironmentVariables.YANDEX_DISK_TOKEN_URL]);

        if(botToken && yandexUrl) return;

        throw new Exception("App configuration is not valid.");        
    }
}