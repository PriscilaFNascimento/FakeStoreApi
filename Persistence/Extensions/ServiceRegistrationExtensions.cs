using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Persistence.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FakeStoreApiDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(FakeStoreApiDbContext).Assembly.FullName)));

            return services;
        }
    }
}
