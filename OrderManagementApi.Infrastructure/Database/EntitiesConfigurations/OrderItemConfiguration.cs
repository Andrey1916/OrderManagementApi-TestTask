using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderManagementApi.Infrastructure.Database.Entities;

namespace OrderManagementApi.Infrastructure.Database.EntitiesConfigurations;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(oi => new { oi.OrderId, oi.ProductName });

        builder.Property(oi => oi.ProductName)
               .IsRequired();
    }
}
