using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class FakeStoreApiDbContext : DbContext
    {
        public FakeStoreApiDbContext(DbContextOptions<FakeStoreApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FakeStoreApiDbContext).Assembly);
        }
    }
}
