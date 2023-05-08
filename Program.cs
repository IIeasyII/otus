using System.Collections;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        var list = new List<int>();
        var arrayList = new ArrayList();
        var linkedList = new LinkedList<int>();
        
        #region Insert performance
        System.Console.WriteLine($"Время на заполнение коллекций");
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        for(var i = 0; i < 1000000; i++)
            list.Add(i);
        stopwatch.Stop();

        System.Console.WriteLine($"'List<int>' - {stopwatch.ElapsedMilliseconds} milliseconds");
        
        stopwatch.Reset();
        stopwatch.Start();
        for(var i = 0; i < 1000000; i++)
            arrayList.Add(i);
        stopwatch.Stop();

        System.Console.WriteLine($"'ArrayList' - {stopwatch.ElapsedMilliseconds} milliseconds");

        stopwatch.Reset();
        stopwatch.Start();
        for(var i = 0; i < 1000000; i++)
            linkedList.AddLast(i);
        stopwatch.Stop();

        System.Console.WriteLine($"'LinkedList<int>' - {stopwatch.ElapsedMilliseconds} milliseconds");
        #endregion

        #region Search element
        System.Console.WriteLine($"Поиск элемента коллекций");
        stopwatch.Reset();
        stopwatch.Start();
        var element = list[496753];
        stopwatch.Stop();
        System.Console.WriteLine($@"'List<int>' если брать по индексу, то операция выполняется за одну команду, счет идет на наносекунды и скорее всего зависимости от мощности машины {stopwatch.ElapsedMilliseconds} milliseconds");

        stopwatch.Reset();
        stopwatch.Start();
        element = list.Find(x => x == 496753);
        stopwatch.Stop();

        System.Console.WriteLine($@"'List<int>' method 'Find': {stopwatch.ElapsedMilliseconds} milliseconds");

        stopwatch.Reset();
        stopwatch.Start();
        element = list.BinarySearch(496753);
        stopwatch.Stop();

        System.Console.WriteLine($@"'List<int>' method 'BinarySearch': {stopwatch.ElapsedMilliseconds} milliseconds");

        stopwatch.Reset();
        stopwatch.Start();
        element = arrayList.BinarySearch(496753);
        stopwatch.Stop();

        System.Console.WriteLine($@"'ArrayList' method 'BinarySearch': {stopwatch.ElapsedMilliseconds} milliseconds");
        #endregion

        #region Element % 777
        System.Console.WriteLine($"Вывод времени выполнения поиска всех элементов деления на 777 без остатка");
        stopwatch.Reset();
        stopwatch.Start();
        foreach(var item in list)
        {
            if(item % 777 != 0) continue;
            System.Console.WriteLine(item);
        }
        stopwatch.Stop();
        System.Console.WriteLine($"'List<int>' %777: {stopwatch.ElapsedMilliseconds} milliseconds");
        
        stopwatch.Reset();
        stopwatch.Start();
        foreach(int item in arrayList)
        {
            if(item % 777 != 0) continue;
            System.Console.WriteLine(item);
        }
        stopwatch.Stop();
        System.Console.WriteLine($"'ArrayList' %777: {stopwatch.ElapsedMilliseconds} milliseconds");

        stopwatch.Reset();
        stopwatch.Start();
        foreach(int item in linkedList)
        {
            if(item % 777 != 0) continue;
            System.Console.WriteLine(item);
        }
        stopwatch.Stop();
        System.Console.WriteLine($"'LinkedList<int>' %777: {stopwatch.ElapsedMilliseconds} milliseconds");
        #endregion
        Console.ReadKey();
    }
}