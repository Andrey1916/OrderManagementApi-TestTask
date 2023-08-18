using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderManagementApi.Infrastructure.Database.Entities;

namespace OrderManagementApi.Infrastructure.Database.EntitiesConfigurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Description)
               .IsRequired(false);

        builder.HasMany(o => o.OrderItems)
               .WithOne(oi => oi.Order)
               .HasForeignKey(oi => oi.OrderId);

        builder.Property(o => o.Status)
               .HasConversion(
                   v => (int)v,
                   v => (OrderStatus)v
                   );
    }
}
