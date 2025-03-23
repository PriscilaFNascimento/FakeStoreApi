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

        public async Task<IEnumerable<OrderProduct>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Where(op => op.OrderId == orderId)
                .Include(op => op.Order)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(OrderProduct orderProduct, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(orderProduct, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<OrderProduct> orderProducts, CancellationToken cancellationToken)
        {
            await _dbSet.AddRangeAsync(orderProducts, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
} 