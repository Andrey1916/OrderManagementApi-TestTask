namespace OrderManagementApi.Models;

public record Order
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
    public string DeliveryAddress { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public Guid CustomerId { get; set; }
    public OrderStatus Status { get; set; }
}

public enum OrderStatus
{
    Created,
    InProcess,
    Delivered
}