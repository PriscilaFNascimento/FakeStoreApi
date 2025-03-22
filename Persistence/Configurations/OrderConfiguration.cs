using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Number)
                .ValueGeneratedOnAdd();

            builder.HasOne(o => o.Costumer)
                .WithMany()
                .HasForeignKey(o => o.CostumerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
} 