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

            builder.HasKey(ci => ci.Id);

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