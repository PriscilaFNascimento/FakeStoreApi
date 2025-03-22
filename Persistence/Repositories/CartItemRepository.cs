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

        public async Task<IEnumerable<CartItemResponseDto>> GetByCostumerIdAsync(Guid costumerId)
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

        public async Task AddAsync(CartItem cartItem)
        {
            await _dbSet.AddAsync(cartItem);
        }

        public async Task UpdateAsync(CartItem cartItem)
        {
            _dbSet.Update(cartItem);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(CartItem cartItem)
        {
            _dbSet.Remove(cartItem);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
} 