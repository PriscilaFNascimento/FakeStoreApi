using Domain.Dtos;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<OrderResponseDto>> GetAllByCostumerIdAsync(Guid costumerId, CancellationToken cancellationToken);
    }
} 