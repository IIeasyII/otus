using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var top = list.Top(111);

        var persons = new List<Person>
        {
            new Person {Name = "Person 1", Age = 12},
            new Person {Name = "Person 2", Age = 42},
            new Person {Name = "Person 3", Age = 32},
            new Person {Name = "Person 4", Age = 13},
            new Person {Name = "Person 5", Age = 17},
            new Person {Name = "Person 6", Age = 10},
        };

        var items = persons.Top(30, person => person.Age);
    }
}

internal class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

internal static class IEnumerableExtension
{
    public static IEnumerable<T> Top<T>(this IEnumerable<T> enumerable, int percent)
    {
        Requires.Range(percent, (x) => x >= 1 && x <= 100);

        var count = (int)Math.Ceiling((double)enumerable.Count() * percent / 100);
        var items = enumerable.TakeLast(count).Reverse();
        return items;
    }

    public static IEnumerable<TSource> Top<TSource, TKey>(this IEnumerable<TSource> enumerable, int percent, Func<TSource, TKey> keySelector)
    {
        Requires.Range(percent, (x) => x >= 1 && x <= 100);

        var count = (int)Math.Ceiling((double)enumerable.Count() * percent / 100);
        var items = enumerable.OrderByDescending<TSource, TKey>(keySelector).Take(count);
        return items;
    }
}

internal static class Requires
{
    public static void Range(int value, Func<int, bool> condition)
    {
        if (!condition(value))
        {
            throw new ArgumentException();
        }
    }
}