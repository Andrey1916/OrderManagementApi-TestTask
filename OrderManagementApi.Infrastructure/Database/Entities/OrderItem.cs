namespace OrderManagementApi.Infrastructure.Database.Entities;

public class OrderItem
{
    public string ProductName { get; set; } = null!;
    public uint Count { get; set; }
    public decimal Price { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
}


/*
 TODO: In real life use product id and combined key orderId + productId
 TODO: Migrations
 */