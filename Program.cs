using System.Diagnostics;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            System.Console.WriteLine("Введите дерево:");
            BinaryTree binaryTree = new BinaryTree();
            while (true)
            {
                var name = Console.ReadLine();
                if (string.IsNullOrEmpty(name)) break;

                decimal.TryParse(Console.ReadLine(), out var salary);

                var employee = new EmployeeInfo(name, salary);
                binaryTree.Add(employee);
            }

            var sortedEmployeers = binaryTree.SymmetricTraversal();
            foreach (var item in sortedEmployeers)
                System.Console.WriteLine($"{item.Name}-{item.Salary}");

            var isFind = true;
            while (isFind)
            {            
                FindInBinaryTree(binaryTree);
                System.Console.WriteLine("0 - создать новое дерево");
                System.Console.WriteLine("1 - новый поиск");

                if (Console.ReadLine() == "0")
                    isFind = false;
            }
        }
    }

    private static void FindInBinaryTree(BinaryTree binaryTree)
    {
        System.Console.WriteLine("Поиск по зарплате. Введите размер зарплаты:");
        var find = Console.ReadLine();
        if (find is null) return;

        var salary = decimal.Parse(find);

        var employee = binaryTree.Find(salary);
        if (employee is null)
            System.Console.WriteLine("Такой сотрудник не найден");
        else
            System.Console.WriteLine(employee.Name);
    }
}

internal class BinaryTree
{
    public Node? Root { get; set; }

    public bool Add(EmployeeInfo employee)
    {
        Node before = null!;
        Node? after = Root;

        while (after != null)
        {
            before = after;
            if (employee.Salary < after.EmployeeInfo.Salary)
            {
                after = after.LeftNode;
            }
            else if (employee.Salary > after.EmployeeInfo.Salary)
            {
                after = after.RightNode;
            }
            else
            {
                return false;
            }
        }

        Node node = new Node(employee);
        if (Root == null)
            Root = node;
        else
        {
            if (employee.Salary < before.EmployeeInfo.Salary)
                before.LeftNode = node;
            if (employee.Salary > before.EmployeeInfo.Salary)
                before.RightNode = node;
        }

        return true;
    }

    public EmployeeInfo? Find(decimal salary)
    {
        var node = Root;
        if(node is null) return null;        

        while(node is not null)
        {
            if(node.EmployeeInfo.Salary == salary)
                return node.EmployeeInfo;
            if(node.LeftNode is not null && node.EmployeeInfo.Salary > salary)
            {
                node = node.LeftNode;
                continue;
            }
            if(node.RightNode is not null && node.EmployeeInfo.Salary < salary)
            {
                node = node.RightNode;
                continue;
            }
            return null;
        }

        return null;
    }

    public IEnumerable<EmployeeInfo> SymmetricTraversal()
    {
        var stack = new Stack<Node>();
        var node = Root;

        while (stack.Count > 0 || node is not null)
        {
            if (node is not null)
            {
                stack.Push(node);
                node = node.LeftNode;
            }
            else
            {
                node = stack.Pop();
                yield return node.EmployeeInfo;
                node = node.RightNode;
            }
        }
    }
}

internal class Node
{
    public Node? LeftNode { get; set; }
    public Node? RightNode { get; set; }
    public EmployeeInfo EmployeeInfo { get; set; }

    public Node(EmployeeInfo root)
    {
        EmployeeInfo = root;
    }
}

internal class EmployeeInfo
{
    public string Name { get; }
    public decimal Salary { get; }

    public EmployeeInfo(string name, decimal salary)
    {
        Name = name;
        Salary = salary;
    }
}