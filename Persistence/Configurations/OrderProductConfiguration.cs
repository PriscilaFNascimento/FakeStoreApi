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

            builder.HasKey(op => new { op.OrderId, op.ProductId });

            builder.Property(ci => ci.ProductId)
                .IsRequired();

            builder.Property(ci => ci.ProductTitle)
                .IsRequired();

            builder.Property(ci => ci.ProductDescription)
                .IsRequired();

            builder.Property(ci => ci.ProductCategory)
                .IsRequired();

            builder.Property(ci => ci.ProductImage)
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