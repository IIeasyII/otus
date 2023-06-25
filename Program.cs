using System.Diagnostics;
using System.Linq;
using System.Net;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var imageDownloader = new ImageDownloader();
        imageDownloader.ImageStarted += () =>
        {
            System.Console.WriteLine("Скачивание файла началось");
        };
        imageDownloader.ImageCompleted += () =>
        {
            System.Console.WriteLine("Скачивание файла закончилось");
        };
        var task = imageDownloader.DownloadAsync();
        var isRun = true;
        while (isRun)
        {
            System.Console.WriteLine("Нажмите клавишу А для выхода или любую другую клавишу для проверки статуса скачивания");
            var key = Console.ReadKey();
            if(key.KeyChar == 'А')
            {
                isRun = false;
                continue;
            }

            var isCompleted = task.IsCompleted;
            System.Console.WriteLine();
            System.Console.WriteLine("Файл {0}", task.IsCompleted ? "загружен" : "не загружен");
        }
    }
}

internal class ImageDownloader
{
    public delegate void DownloadEventHandler();
    public event DownloadEventHandler ImageStarted;
    public event DownloadEventHandler ImageCompleted;

    public async Task DownloadAsync()
    {
        ImageStarted();
        string uri = "https://effigis.com/wp-content/uploads/2015/02/Iunctus_SPOT5_5m_8bit_RGB_DRA_torngat_mountains_national_park_8bits_1.jpg";
        string fileName = "bigimage.jpg";
        var webClient = new WebClient();
        System.Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......\n\n", fileName, uri);
        await webClient.DownloadFileTaskAsync(uri, fileName);
        Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", fileName, uri);
        ImageCompleted();
    }
}