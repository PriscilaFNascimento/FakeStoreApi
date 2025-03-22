using Domain.Dtos;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICartItemRepository
    {
        Task<IEnumerable<CartItemResponseDto>> GetByCostumerIdAsync(Guid costumerId, CancellationToken cancellationToken);
        Task<CartItem> GetByCostumerIdAndProductNameAsync(Guid costumerId, string productName, CancellationToken cancellationToken);
        Task AddAsync(CartItem cartItem, CancellationToken cancellationToken);
        Task UpdateAsync(CartItem cartItem, CancellationToken cancellationToken);
        Task DeleteAsync(CartItem cartItem, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
} 