using Domain.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        private readonly DbSet<CartItem> _dbSet;

        public CartItemRepository(FakeStoreApiDbContext context) : base(context)
        {
            _dbSet = context.Set<CartItem>();
        }

        public async Task<IEnumerable<CartItemResponseDto>> GetByCostumerIdAsync(Guid costumerId, CancellationToken cancellationToken)
        {
            var query = from c in _dbSet
                        where c.CostumerId == costumerId
                        select new CartItemResponseDto
                        {
                            Id = c.Id,
                            CostumerId = c.CostumerId,
                            ProductName = c.ProductName,
                            Quantity = c.Quantity,
                            ProductPrice = c.ProductPrice
                        };

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<CartItem> GetByCostumerIdAndProductNameAsync(Guid costumerId, string productName, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.CostumerId == costumerId && c.ProductName == productName, cancellationToken);
        }
    }
} 