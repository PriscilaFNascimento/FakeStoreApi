using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CostumerConfiguration : IEntityTypeConfiguration<Costumer>
    {
        public void Configure(EntityTypeBuilder<Costumer> builder)
        {
            builder.ToTable(nameof(Costumer));

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
} 