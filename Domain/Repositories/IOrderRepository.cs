using Domain.Entities;

namespace Domain.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetByCostumerIdAsync(Guid costumerId);
    }
} 