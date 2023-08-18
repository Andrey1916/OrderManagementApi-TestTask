namespace OrderManagementApi.Models;

public record NewOrder
{
    public string? Description { get; set; }
    public string DeliveryAddress { get; set; } = null!;

    public Guid CustomerId { get; set; }

    public IEnumerable<OrderItem> OrderItems { get; set; } = null!;
}
