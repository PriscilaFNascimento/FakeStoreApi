using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class FakeStoreApiDbContext : DbContext
    {
        public FakeStoreApiDbContext(DbContextOptions<FakeStoreApiDbContext> options) : base(options)
        {
            
        }

        public DbSet<Costumer> Costumers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FakeStoreApiDbContext).Assembly);
        }
    }
}
