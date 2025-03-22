using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable(nameof(CartItem));

            builder.HasKey(ci => new { ci.CostumerId, ci.ProductName });

            builder.Property(ci => ci.ProductName)
                .IsRequired();

            builder.Property(ci => ci.ProductQuantity)
                .IsRequired();

            builder.Property(ci => ci.ProductPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.HasOne(ci => ci.Costumer)
                .WithMany()
                .HasForeignKey(ci => ci.CostumerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Ignore(ci => ci.TotalPrice);
        }
    }
} 