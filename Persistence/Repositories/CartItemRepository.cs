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

        public async Task<IEnumerable<CartItem>> GetByCostumerIdAsync(Guid costumerId)
        {
            return await _dbSet
                .Where(ci => ci.CostumerId == costumerId)
                .Include(ci => ci.Costumer)
                .ToListAsync();
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