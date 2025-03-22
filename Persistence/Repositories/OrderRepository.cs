using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(FakeStoreApiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetByCostumerIdAsync(Guid costumerId)
        {
            return await _dbSet
                .Where(o => o.CostumerId == costumerId)
                .Include(o => o.Costumer)
                .ToListAsync();
        }
    }
} 