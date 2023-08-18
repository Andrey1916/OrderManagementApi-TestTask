namespace OrderManagementApi.Models;

public record OrderDetails
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
    public string DeliveryAddress { get; set; } = null!;
    public OrderStatus Status { get; set; }

    public Guid CustomerId { get; set; }

    public IEnumerable<OrderItem> OrderItems { get; set; } = null!;
}

