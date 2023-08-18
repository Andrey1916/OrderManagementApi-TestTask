namespace OrderManagementApi.Infrastructure.Database.Entities;

public record Customer
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Email { get; set; }
    public string PhoneNumber { get; set; } = null!;

    public uint OrdersCount { get; set; }

    public ICollection<Order> Orders { get; set; } = null!;
}
