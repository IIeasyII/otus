using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using TelegramBot.Console.YandexDisk.Services;
using TelegramBot.Extensions;
using YandexDisk.Client;
using YandexDisk.Client.Http;

namespace TelegramBot.Console;

internal class TelegramBot
{
    private static async Task Main(string[] args)
    {
        try
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            await TelegramBot.RunAsync(args, configuration);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.ToString());
        }
    }

    public static async Task RunAsync(string[] arg, IConfiguration configuration)
    {
        configuration.Validate();

        var bot = new YandexDiskBot(configuration);
        await bot.StartAsync();
    }
}