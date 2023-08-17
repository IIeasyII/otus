using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var librarian = new Librarian();
        var menu = new Menu(librarian);

        var isRun = true;
        while (isRun)
        {
            menu.Show();

            var readline = Console.ReadLine();

            if (!int.TryParse(readline, out int id)) continue;

            var command = (EMenuCommand)id;

            switch (command)
            {
                case EMenuCommand.Add:
                    {
                        System.Console.WriteLine("Введите название книги");
                        var book = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(book)) continue;

                        menu.AddBook(book);
                        continue;
                    }
                case EMenuCommand.Status:
                    {
                        menu.GetStatus();
                        continue;
                    }
                case EMenuCommand.Exit:
                    {
                        isRun = false;
                        break;
                    }
            }
        }

    }
}

internal class Librarian
{
    public Librarian()
    {
        Read();
    }

    private ConcurrentDictionary<string, int> _dictionary { get; } = new();

    public void AddBook(string name)
    {
        var isContains = _dictionary.ContainsKey(name);
        if (isContains) return;

        _dictionary.TryAdd(name, 0);
    }

    public void GetStatus()
    {
        foreach (var item in _dictionary)
        {
            var book = item.Key;
            var status = item.Value;
            System.Console.WriteLine($"{book} - {status}");
        }
    }

    private void Read()
    {
        Task.Run(async () =>
        {
            while (true)
            {
                foreach (var item in _dictionary)
                {
                    var book = item.Key;
                    var status = item.Value;

                    if (status > 99)
                    {
                        _dictionary.Remove(book, out _);
                        continue;
                    }

                    _dictionary[book] += 1;

                }
                await Task.Delay(200);
            }
        });
    }
}

internal class Menu
{
    private readonly Librarian _librarian;

    public Menu(Librarian librarian)
    {
        _librarian = librarian;
    }

    public void AddBook(string name)
    {
        _librarian.AddBook(name);
    }

    public void GetStatus()
    {
        _librarian.GetStatus();
    }

    public void Show()
    {
        foreach (EMenuCommand item in Enum.GetValues(typeof(EMenuCommand)))
        {
            System.Console.WriteLine($"{(int)item}. {item}");
        }
    }
}

internal enum EMenuCommand
{
    Add = 1,
    Status = 2,
    Exit = 3
}