using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

internal class Program
{
    private static async Task Main(string[] args)
    {
        IPart part1 = new Part1();
        IPart part2 = new Part2();
        IPart part3 = new Part3();
        IPart part4 = new Part4();
        IPart part5 = new Part5();
        IPart part6 = new Part6();
        IPart part7 = new Part7();
        IPart part8 = new Part8();
        IPart part9 = new Part9();

        var empty = ImmutableList.CreateBuilder<string>();
        part1.AddPart(empty.ToImmutable());
        part2.AddPart(part1.Poem);
        part3.AddPart(part2.Poem);
        part4.AddPart(part3.Poem);
        part5.AddPart(part4.Poem);
        part6.AddPart(part5.Poem);
        part7.AddPart(part6.Poem);
        part8.AddPart(part7.Poem);
        part9.AddPart(part8.Poem);

        System.Console.WriteLine(String.Join(" ", part1.Poem));
        System.Console.WriteLine("===================================================================");        
        System.Console.WriteLine(String.Join("\n", part2.Poem));
        System.Console.WriteLine("===================================================================");        
        System.Console.WriteLine(String.Join("\n", part3.Poem));
        System.Console.WriteLine("===================================================================");        
        System.Console.WriteLine(String.Join("\n", part4.Poem));
        System.Console.WriteLine("===================================================================");        
        System.Console.WriteLine(String.Join("\n", part5.Poem));
        System.Console.WriteLine("===================================================================");        
        System.Console.WriteLine(String.Join("\n", part6.Poem));
        System.Console.WriteLine("===================================================================");        
        System.Console.WriteLine(String.Join("\n", part7.Poem));
        System.Console.WriteLine("===================================================================");        
        System.Console.WriteLine(String.Join("\n", part8.Poem));
        System.Console.WriteLine("===================================================================");        
        System.Console.WriteLine(String.Join("\n", part9.Poem));
    }
}

internal interface IPart
{
    public void AddPart(ImmutableList<string> collection)
    {
        Poem = collection.Add(SelfPart);
    }

    ImmutableList<string> Poem { get; set; }

    string SelfPart { get; }
}

internal class Part1 : IPart
{
    public ImmutableList<string> Poem { get; set; }
    public string SelfPart => "Вот дом, Который построил Джек.";
}
internal class Part2 : IPart
{
    public ImmutableList<string> Poem { get; set; }

    public string SelfPart => "А это пшеница, Которая в темном чулане хранится В доме, Который построил Джек.";
}
internal class Part3 : IPart
{
    public ImmutableList<string> Poem { get; set; }

    public string SelfPart => "А это веселая птица-синица, Которая часто ворует пшеницу, Которая в темном чулане хранится В доме, Который построил Джек.";
}
internal class Part4 : IPart
{
    public ImmutableList<string> Poem { get; set; }
    public string SelfPart => "Вот кот, Который пугает и ловит синицу, Которая часто ворует пшеницу, Которая в темном чулане хранится В доме, Который построил Джек.";
}
internal class Part5 : IPart
{
    public ImmutableList<string> Poem { get; set; }
    public string SelfPart => "Вот пес без хвоста, Который за шиворот треплет кота, Который пугает и ловит синицу, Которая часто ворует пшеницу, Которая в темном чулане хранится В доме, Который построил Джек.";
}
internal class Part6 : IPart
{
    public ImmutableList<string> Poem { get; set; }
    public string SelfPart => "А это корова безрогая, Лягнувшая старого пса без хвоста, Который за шиворот треплет кота, Который пугает и ловит синицу, Которая часто ворует пшеницу, Которая в темном чулане хранится В доме, Который построил Джек.";
}
internal class Part7 : IPart
{
    public ImmutableList<string> Poem { get; set; }
    public string SelfPart => "А это старушка, седая и строгая, Которая доит корову безрогую, Лягнувшую старого пса без хвоста, Который за шиворот треплет кота, Который пугает и ловит синицу, Которая часто ворует пшеницу, Которая в темном чулане хранится В доме, Который построил Джек.";
}
internal class Part8 : IPart
{
    public ImmutableList<string> Poem { get; set; }
    public string SelfPart => "А это ленивый и толстый пастух, Который бранится с коровницей строгою, Которая доит корову безрогую, Лягнувшую старого пса без хвоста, Который за шиворот треплет кота, Который пугает и ловит синицу, Которая часто ворует пшеницу, Которая в темном чулане хранится В доме, Который построил Джек.";
}
internal class Part9 : IPart
{
    public ImmutableList<string> Poem { get; set; }
    public string SelfPart => "Вот два петуха, Которые будят того пастуха, Который бранится с коровницей строгою, Которая доит корову безрогую, Лягнувшую старого пса без хвоста, Который за шиворот треплет кота, Который пугает и ловит синицу, Которая часто ворует пшеницу, Которая в темном чулане хранится В доме, Который построил Джек.";
}