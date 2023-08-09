using System.Collections.ObjectModel;
using System.Collections.Specialized;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var shop = new Shop();
        shop.Add(new ShopItem(1, "hello"));

        var customer = new Customer();
        customer.Subscribe(shop);

        ConsoleKey key;
        var countShopItems = 0;
        while((key = Console.ReadKey().Key) is not ConsoleKey.X)
        {
            if(key is ConsoleKey.A)
            {
                var content = $"Товар от {DateTime.Now}";
                var item = new ShopItem(countShopItems, content);
                shop.Add(item);
                countShopItems++;
            }
            if(key is ConsoleKey.D)
            {
                System.Console.WriteLine("Какой товар удалить?");
                var id = Convert.ToInt64(Console.ReadLine());                
                shop.Remove(id);
            }
        }
    }
}

internal class ShopItem
{
    public long Id { get; set; }
    public string Name { get; set; }

    public ShopItem(long id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() => $"id={Id}, name={Name}";
}

internal class Shop
{
    private ObservableCollection<ShopItem> _items { get; set; } = new();

    public void Subscribe(NotifyCollectionChangedEventHandler handler)
    {
        _items.CollectionChanged += handler;
    }

    public void Add(ShopItem item)
    {
        _items.Add(item);
    }

    public void Remove(long id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if(item is null) return;

        _items.Remove(item);
    }

}

internal class Customer
{
    public void Subscribe(Shop shop)
    {
        shop.Subscribe(OnItemChanged);
    }

    public void OnItemChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.NewItems?[0] is ShopItem newItem)
                    System.Console.WriteLine($"{e.Action} item {newItem}");
                break;
            case NotifyCollectionChangedAction.Remove:
                if (e.OldItems?[0] is ShopItem oldItem)
                    Console.WriteLine($"{e.Action} item {oldItem}");
                break;
            case NotifyCollectionChangedAction.Replace: // если замена
                if ((e.NewItems?[0] is ShopItem replacingItem) &&
                    (e.OldItems?[0] is ShopItem replacedItem))
                    Console.WriteLine($"{e.Action} item {replacedItem} replace on {replacingItem}");
                break;
        }
        
    }
}