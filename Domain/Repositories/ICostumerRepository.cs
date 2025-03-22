using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICostumerRepository : IBaseRepository<Costumer>
    {
        Task<Costumer> GetByEmailAsync(string email);
    }
} 