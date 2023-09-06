using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using Otus;

internal class Program
{
    private static async Task Main(string[] args)
    {

        var connectionString = "Host=localhost;Database=Shop;Username=postgres;password=easy";
        var shopRepository = new ShopRepository(connectionString);
        var firstNames = shopRepository.GetCustomersByFirstName("Павел");
        var lastNames = shopRepository.GetCustomersByLastName("Хомякова");
        var ages = shopRepository.GetCustomersByAge(20);
        var info = shopRepository.GetShopInfo(1, 30);
        System.Console.WriteLine($@"|CustomerId|FirstName|LastName|ProductId|ProductQuantity|ProductPrice|");
        foreach (var item in info)
        {
            for (int i = 0; i < item.Products.Count; i++)
            {
                System.Console.WriteLine($@"|{item.Id}|{item.FirstName}|{item.LastName}|{item.Products[i].Id}|{item.Orders[i].Quantity}|{item.Products[i].Price}|");
            }
        }
    }
}

internal class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int Age { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
    public List<Order> Orders { get; set; } = new List<Order>();
}

internal class Order
{
    public int Id { get; set; }
    public int CustomersId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public Customer Customer { get; set; } = null!;
    public Product Product { get; set; } = null!;
}

internal class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int StockQuantity { get; set; }
    public int? Price { get; set; }
    public List<Customer> Customers { get; set; } = null!;
}