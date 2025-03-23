using Domain.Dtos;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICartItemRepository : IBaseRepository<CartItem>
    {
        Task<List<CartItemResponseDto>> GetAllByCostumerIdAsync(Guid costumerId, CancellationToken cancellationToken);
        Task<CartItem> GetByCostumerIdAndProductIdAsync(Guid costumerId, int productId, CancellationToken cancellationToken);
        public void DeleteAllByCostumerId(Guid costumerId);
    }
} 