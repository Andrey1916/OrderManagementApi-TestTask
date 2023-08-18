using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderManagementApi.Infrastructure.Database.Entities;

namespace OrderManagementApi.Infrastructure.Database.EntitiesConfigurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
               .IsRequired();

        builder.Property(c => c.LastName)
               .IsRequired();

        builder.Property(c => c.PhoneNumber)
               .IsRequired();


        builder.HasMany(c => c.Orders)
               .WithOne(o => o.Customer)
               .HasForeignKey(o => o.CustomerId);
    }
}
