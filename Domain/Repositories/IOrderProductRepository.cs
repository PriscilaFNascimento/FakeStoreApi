using Domain.Entities;

namespace Domain.Repositories
{
    public interface IOrderProductRepository
    {
        Task<IEnumerable<OrderProduct>> GetByOrderIdAsync(Guid orderId);
        Task AddAsync(OrderProduct orderProduct);
        Task SaveChangesAsync();
    }
} 