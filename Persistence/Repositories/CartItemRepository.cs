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

        public async Task<List<CartItemResponseDto>> GetAllByCostumerIdAsync(Guid costumerId, CancellationToken cancellationToken)
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

        public void DeleteAllByCostumerId(Guid costumerId)
        {
            var cartItems = _dbSet.Where(c => c.CostumerId == costumerId);
            _dbSet.RemoveRange(cartItems);
        }
    }
} 