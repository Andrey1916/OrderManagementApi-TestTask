namespace OrderManagementApi.Models;

public record OrderItem
{
    public string ProductName { get; set; } = null!;
    public uint Count { get; set; }
    public decimal Price { get; set; }
}
