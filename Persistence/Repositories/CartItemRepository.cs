using Domain.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly FakeStoreApiDbContext _context;
        private readonly DbSet<CartItem> _dbSet;

        public CartItemRepository(FakeStoreApiDbContext context)
        {
            _context = context;
            _dbSet = context.Set<CartItem>();
        }

        public async Task<IEnumerable<CartItemResponseDto>> GetByCostumerIdAsync(Guid costumerId, CancellationToken cancellationToken)
        {
            var query = from c in _dbSet
                        where c.CostumerId == costumerId
                        select new CartItemResponseDto
                        {
                            CostumerId = c.CostumerId,
                            ProductName = c.ProductName,
                            Quantity = c.Quantity,
                            ProductPrice = c.ProductPrice
                        };

            return await query.ToListAsync();
        }

        public async Task<CartItem> GetByCostumerIdAndProductNameAsync(Guid costumerId, string productName, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.CostumerId == costumerId && c.ProductName == productName, cancellationToken);
        }

        public async Task AddAsync(CartItem cartItem, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(cartItem, cancellationToken);
        }

        public async Task UpdateAsync(CartItem cartItem, CancellationToken cancellationToken)
        {
            _dbSet.Update(cartItem);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(CartItem cartItem, CancellationToken cancellationToken)
        {
            _dbSet.Remove(cartItem);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
} 