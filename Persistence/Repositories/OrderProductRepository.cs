using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly FakeStoreApiDbContext _context;
        private readonly DbSet<OrderProduct> _dbSet;

        public OrderProductRepository(FakeStoreApiDbContext context)
        {
            _context = context;
            _dbSet = context.Set<OrderProduct>();
        }

        public async Task<IEnumerable<OrderProduct>> GetByOrderIdAsync(Guid orderId)
        {
            return await _dbSet
                .Where(op => op.OrderId == orderId)
                .Include(op => op.Order)
                .ToListAsync();
        }

        public async Task AddAsync(OrderProduct orderProduct)
        {
            await _dbSet.AddAsync(orderProduct);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
} 