using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICartItemRepository
    {
        Task<IEnumerable<CartItem>> GetByCostumerIdAsync(Guid costumerId);
        Task AddAsync(CartItem cartItem);
        Task UpdateAsync(CartItem cartItem);
        Task DeleteAsync(CartItem cartItem);
        Task SaveChangesAsync();
    }
} 