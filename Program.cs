using System.Text;

internal class Program
{
    private const char Symbol = '+';

    private static void Main(string[] args)
    {
        System.Console.WriteLine("Введите размерность таблицы:");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || (n < 1 || n > 6))
        {
            Console.Clear();
            System.Console.WriteLine("Введите размерность таблицы:");
        }

        System.Console.WriteLine("Введите произвольный текст:");

        string? text;
        int limit = 40;
        int width;
        while (string.IsNullOrWhiteSpace(text = Console.ReadLine()) || (width = text.Length + n * 2) > limit)
        {
            System.Console.WriteLine("Введите произвольный текст:");
        }

        System.Console.Clear();

        var horizontal = new string(Symbol, width);

        for (int i = 0; i < 7; i++)
        {
            switch (i)
            {
                case 1:
                    {
                        PrintTop(text, width, n);
                        break;
                    }
                case 3:
                    {
                        PrintCenter(width);
                        break;
                    }
                case 5:
                    {
                        PrintBottom(width);
                        break;
                    }
                default:
                    System.Console.WriteLine(horizontal);
                    break;
            }
        }

        Console.ReadKey();
    }

    static void PrintTop(string text, int width, int n)
    {
        var indent = n - 1;
        var empty = new string(' ', indent);
        var row = $"{Symbol}{new string(' ', width - 2)}{Symbol}\n";
        var half = $"{string.Join(string.Empty, Enumerable.Repeat(row, indent))}";
        var table = $"{half}{Symbol}{empty}{text}{empty}{Symbol}\n{half}";

        System.Console.Write(table);
    }

    static void PrintCenter(int width)
    {
        for (int i = 0; i < width; i++)
        {
            System.Console.Write(Symbol);
            for (int j = 0; j < width - 2; j++)
            {
                Console.Write((i + j) % 2 == 0 ? ' ' : Symbol);
            }
            System.Console.Write(Symbol);
            Console.WriteLine();
        }
    }

    static void PrintBottom(int width)
    {
        if (width < 5)
        {
            for (int i = 0; i < width - 2; i++)
                System.Console.WriteLine($"{new string(Symbol, width)}");
            return;
        }
        var edges = 0;
        var center = width - 4;
        for (; center > 0; edges++, center -= 2)
            System.Console.WriteLine($"{Symbol}{new string(' ', edges)}{Symbol}{new string(' ', center)}{Symbol}{new string(' ', edges)}{Symbol}");
        System.Console.WriteLine($"{Symbol}{new string(' ', edges)}{(width % 2 == 0 ? $"{Symbol}{Symbol}" : Symbol)}{new string(' ', edges)}{Symbol}");
        for (edges--, center += 2; center < width - 2; edges--, center += 2)
            System.Console.WriteLine($"{Symbol}{new string(' ', edges)}{Symbol}{new string(' ', center)}{Symbol}{new string(' ', edges)}{Symbol}");
    }
}