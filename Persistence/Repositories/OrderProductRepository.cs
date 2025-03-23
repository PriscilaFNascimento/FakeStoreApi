using Domain.Dtos;
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

        public async Task<IEnumerable<OrderProductResponseDto>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var query = from op in _dbSet
                        where op.OrderId == orderId
                        select new OrderProductResponseDto
                        {
                            OrderId = op.OrderId,
                            ProductId = op.ProductId,
                            ProductTitle = op.ProductTitle,
                            ProductDescription = op.ProductDescription,
                            ProductCategory = op.ProductCategory,
                            ProductPrice = op.ProductPrice,
                            ProductImage = op.ProductImage,
                            ProductQuantity = op.ProductQuantity
                        };

            return await query.ToListAsync();
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