using System.Collections;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        System.Console.WriteLine("a*x^2+b*x+c=0");
        Dictionary<char, int> arguments;
        while (!TryReadParameters(out arguments)) ;

        var quadraticEquation = new QuadraticEquation(arguments);

        try
        {
            var result = quadraticEquation.Calculate();
            System.Console.WriteLine(result);
        }
        catch (NoEquationRootsException ex)
        {
            FormatData(ex.Message, Severity.Warning, null!);
        }

        Console.ReadKey();
    }

    private static bool TryReadParameters(out Dictionary<char, int> arguments)
    {
        arguments = new();
        var parameters = new Dictionary<char, string>()
        {
            {'a', string.Empty},
            {'b', string.Empty},
            {'c', string.Empty},
        };

        //read parameter from console
        foreach (var item in parameters.Keys)
        {
            var message = $"Введите значение {item}:";
            System.Console.WriteLine(message);
            var input = Console.ReadLine();
            parameters[item] = input;
        }

        try
        {
            foreach (var item in parameters)
            {
                var parameter = item.Key;
                var value = item.Value;

                if (!int.TryParse(value, out var number))
                    continue;

                parameters.Remove(parameter);
                arguments.Add(parameter, number);
            }
            if (parameters.Keys.Count > 0)
                throw new FormatException("Can't parse parameters");
        }
        catch (FormatException ex)
        {
            FormatData(ex.Message, Severity.Error, parameters);
            return false;
        }

        return true;
    }

    private static void FormatData(string message, Severity severity, IDictionary data)
    {
        switch (severity)
        {
            case Severity.Error:
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    foreach (var key in data.Keys)
                        System.Console.WriteLine($"{key} - {data[key]}");
                    break;
                }
            case Severity.Warning:
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    System.Console.WriteLine(message);
                    break;
                }
        }
        Console.ResetColor();
    }

    enum Severity
    {
        Warning,
        Error
    }
}

internal class NoEquationRootsException : Exception
{
    public NoEquationRootsException(string message) : base(message)
    {

    }
}

internal class QuadraticEquation
{
    private readonly Dictionary<char, int> _arguments;

    public QuadraticEquation(Dictionary<char, int> arguments)
    {
        _arguments = arguments;
    }

    public string Calculate()
    {
        var a = _arguments['a'];
        var b = _arguments['b'];
        var c = _arguments['c'];

        var discriminant = GetDiscriminant(a, b, c);
        var answer = string.Empty;

        switch (discriminant)
        {
            case var expression when discriminant < 0:
                {
                    throw new NoEquationRootsException("Вещественных значений не найдено.");
                }
            case var expression when discriminant == 0:
                {
                    var x = (-b + Math.Sqrt(discriminant)) / 2 * a;
                    answer = $"x = {x}";
                    break;
                }
            case var expression when discriminant > 0:
                {
                    var x1 = (-b + Math.Sqrt(discriminant)) / 2 * a;
                    var x2 = (-b - Math.Sqrt(discriminant)) / 2 * a;
                    answer = $"x1 = {x1}, x2 = {x2}";
                    break;
                }
        }

        return answer;
    }

    private double GetDiscriminant(int a, int b, int c) => Math.Pow(b, 2) - 4 * a * c;
}