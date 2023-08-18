using Microsoft.EntityFrameworkCore;
using OrderManagementApi.Infrastructure.Database.Entities;
using OrderManagementApi.Infrastructure.Database.EntitiesConfigurations;

namespace OrderManagementApi.Infrastructure.Database;

public class DataContext : DbContext
{
    public DataContext()
    { }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());

        Seed(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    private static void Seed(ModelBuilder modelBuilder)
    {
        var customerId = Guid.NewGuid();
        var orderId = Guid.NewGuid();

        modelBuilder.Entity<Customer>()
            .HasData(
                new Customer
                {
                    Id          = customerId,
                    FirstName   = "FName1",
                    LastName    = "LName1",
                    PhoneNumber = "1234567890",
                    Email       = "mail@mail.com",
                }
            );

        modelBuilder.Entity<Order>()
            .HasData(
                new Order
                {
                    Id              = orderId,
                    CustomerId      = customerId,
                    DeliveryAddress = "Some Address",
                    Description     = "Description",
                    CreateDate      = DateTime.UtcNow,
                    UpdateDate      = DateTime.UtcNow,
                    Status          = OrderStatus.Delivered
                }
            );

        modelBuilder.Entity<OrderItem>()
            .HasData(
                new OrderItem
                {
                    Count       = 1,
                    OrderId     = orderId,
                    Price       = 100m,
                    ProductName = "Test",
                },
                new OrderItem
                {
                    Count       = 15,
                    OrderId     = orderId,
                    Price       = 100m,
                    ProductName = "Test3",
                }
            );
    }
}
