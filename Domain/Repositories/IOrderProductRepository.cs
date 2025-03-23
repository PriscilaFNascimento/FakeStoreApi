using Domain.Dtos;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IOrderProductRepository
    {
        Task<IEnumerable<OrderProductResponseDto>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);
        Task AddAsync(OrderProduct orderProduct, CancellationToken cancellationToken);
        Task AddRangeAsync(IEnumerable<OrderProduct> orderProducts, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
} 