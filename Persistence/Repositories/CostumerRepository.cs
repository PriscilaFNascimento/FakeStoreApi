using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class CostumerRepository : BaseRepository<Costumer>, ICostumerRepository
    {
        public CostumerRepository(FakeStoreApiDbContext context) : base(context)
        {
        }

        public async Task<Costumer> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Email == email);
        }
    }
} 