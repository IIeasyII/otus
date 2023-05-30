internal class Program
{
    private static void Main(string[] args)
    {
        var s = new Stack("a", "b", "c");
        // size = 3, Top = 'c'
        Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
        var deleted = s.Pop();
        // Извлек верхний элемент 'c' Size = 2
        Console.WriteLine($"Извлек верхний элемент '{deleted}' Size = {s.Size}");
        s.Add("d");
        // size = 3, Top = 'd'
        Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
        s.Pop();
        s.Pop();
        s.Pop();
        // size = 0, Top = null
        Console.WriteLine($"size = {s.Size}, Top = {(s.Top == null ? "null" : s.Top)}");
        s.Pop();


        var stack1 = new Stack("a", "b", "c");
        stack1.Merge(new Stack("1", "2", "3"));
        // в стеке s теперь элементы - "a", "b", "c", "3", "2", "1" <- верхний

        Console.ReadKey();
    }
}

internal class Stack
{
    private StackItem? _item;
    public int Size { get; set; } = 0;

    public string? Top
    {
        get
        {
            if (Size == 0)
                return null;
            return _item?.Current;
        }
    }

    public Stack(params string[] items)
    {
        Size = items.Length;
        if (Size == 0) return;

        var previous = new StackItem(items[0]) { Previous = null };
        for (int i = 1; i < Size; i++)
        {
            _item = new StackItem(items[i]) { Previous = previous };
            previous = _item;
        }
    }

    public void Add(string item)
    {
        _item = new StackItem(item) { Previous = _item };
        Size++;
    }

    public string Pop()
    {
        if (_item is null)
            throw new Exception("Стек пустой");

        var last = _item.Current;
        _item = _item.Previous;
        Size--;

        return last;
    }

    private class StackItem
    {
        public string Current { get; set; }
        public StackItem? Previous { get; set; }

        public StackItem(string value)
        {
            Current = value;
        }
    }
}

internal static class StackExtensions
{
    public static void Merge(this Stack stack1, Stack stack2)
    {
        while (stack2.Size != 0)
        {
            var item = stack2.Pop();
            stack1.Add(item);
        }
    }
}