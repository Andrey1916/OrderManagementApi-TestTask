namespace OrderManagementApi.Infrastructure.Database.Entities;

public class Order
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
    public string DeliveryAddress { get; set; } = null!;
    public OrderStatus Status { get; set; }

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = null!;
}

public enum OrderStatus
{
    Created,
    InProcess,
    Delivered
}