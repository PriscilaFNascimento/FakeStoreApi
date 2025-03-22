using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable(nameof(OrderProduct));

            builder.HasKey(op => new { op.OrderId, op.ProductName });

            builder.Property(op => op.ProductName)
                .IsRequired();

            builder.Property(op => op.ProductQuantity)
                .IsRequired();

            builder.Property(op => op.ProductPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.HasOne(op => op.Order)
                .WithMany()
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(op => op.TotalPrice);
        }
    }
} 