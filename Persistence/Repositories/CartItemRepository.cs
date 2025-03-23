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
                            ProductTitle = c.ProductTitle,
                            ProductId = c.ProductId,
                            ProductCategory = c.ProductCategory,
                            ProductDescription = c.ProductDescription,
                            ProductImage = c.ProductImage,
                            ProductPrice = c.ProductPrice,
                            Quantity = c.Quantity
                        };

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<CartItem> GetByCostumerIdAndProductIdAsync(Guid costumerId, int productId, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.CostumerId == costumerId && c.ProductId == productId, cancellationToken);
        }

        public void DeleteAllByCostumerId(Guid costumerId)
        {
            var cartItems = _dbSet.Where(c => c.CostumerId == costumerId);
            _dbSet.RemoveRange(cartItems);
        }
    }
} 