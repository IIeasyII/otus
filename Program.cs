internal class Program
{
    private static void Main(string[] args)
    {
        IRobot robot = new Quadcopter();
        foreach (var item in robot.GetComponents())
            System.Console.WriteLine(item);

        System.Console.WriteLine(robot.GetRobotType());
        System.Console.WriteLine(robot.GetInfo());

        IFlyingRobot flyingRobot = new Quadcopter();
        foreach (var item in flyingRobot.GetComponents())
            System.Console.WriteLine(item);

        System.Console.WriteLine(flyingRobot.GetRobotType());
        System.Console.WriteLine(flyingRobot.GetInfo());

        IChargeable chargeable = new Quadcopter();
        chargeable.Charge();
        System.Console.WriteLine(chargeable.GetInfo());

        Quadcopter quadcopter = new Quadcopter();
        System.Console.WriteLine(quadcopter.GetInfo());

        Console.ReadKey();
    }
}

internal interface IRobot
{
    string GetInfo();
    List<string> GetComponents();

    string GetRobotType()
    {
        return "I am a simple robot.";
    }
}

internal interface IChargeable
{
    void Charge();
    string GetInfo();
}

internal interface IFlyingRobot : IRobot
{
    string IRobot.GetRobotType()
    {
        return "I am a flying robot.";
    }
}

internal class Quadcopter : IFlyingRobot, IChargeable
{
    private List<string> _components = new List<string> { "rotor1", "rotor2", "rotor3", "rotor4" };

    public void Charge()
    {
        System.Console.WriteLine("Charging...");
        Thread.Sleep(3000);
        System.Console.WriteLine("Charged!");
    }

    string IRobot.GetInfo()
    {
        return "I am a robot, but i am quadcopter";
    }

    string IChargeable.GetInfo()
    {
        return "I am chargeable quadcopter";
    }

    public List<string> GetComponents()
    {
        return _components;
    }

    public string GetInfo()
    {
        return "I am a quadcopter";
    }
}