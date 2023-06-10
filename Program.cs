using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        var result1 = FibonacciRecursive(10);
        System.Console.WriteLine("FibonacciRecursive=" + result1);
        var result2 = FibonacciRecursive(10);
        System.Console.WriteLine("FibonacciLoop=" + result2);

        Stopwatch stopwatch = new Stopwatch();

        #region 5
        var n = 5;
        stopwatch.Start();
        FibonacciRecursive(n);
        stopwatch.Stop();
        System.Console.WriteLine($"FibonacciRecursive({n})={stopwatch.ElapsedTicks} ticks" );
        stopwatch.Reset();

        stopwatch.Start();
        FibonacciLoop(n);
        stopwatch.Stop();
        System.Console.WriteLine($"FibonacciLoop({n})={stopwatch.ElapsedTicks} ticks" );
        stopwatch.Reset();
        #endregion

        #region 10
        n = 10;
        stopwatch.Start();
        FibonacciRecursive(n);
        stopwatch.Stop();
        System.Console.WriteLine($"FibonacciRecursive({n})={stopwatch.ElapsedTicks} ticks" );
        stopwatch.Reset();

        stopwatch.Start();
        FibonacciLoop(10);
        stopwatch.Stop();
        System.Console.WriteLine($"FibonacciLoop({n})={stopwatch.ElapsedTicks} ticks" );
        stopwatch.Reset();
        #endregion

        #region 20
        n = 20;
        stopwatch.Start();
        FibonacciRecursive(n);
        stopwatch.Stop();
        System.Console.WriteLine($"FibonacciRecursive({n})={stopwatch.ElapsedTicks} ticks" );
        stopwatch.Reset();

        stopwatch.Start();
        FibonacciLoop(n);
        stopwatch.Stop();
        System.Console.WriteLine($"FibonacciLoop({n})={stopwatch.ElapsedTicks} ticks" );
        stopwatch.Reset();
        #endregion
    }

    private static int FibonacciRecursive(int n)
    {
        if(n == 0) return 0;
        if(n == 1) return 1;

        return FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2);
    }

    private static double FibonacciLoop(int n)
    {
        if(n == 0) return 0;
        if(n == 1) return 1;

        int result = 2;
        var current = 1;
        var previous = 2;

        for(int i = 0; i < n; i++)
        {
            result = current + previous;
            previous = current;
            current = result;
        }

        return result;
    }
}