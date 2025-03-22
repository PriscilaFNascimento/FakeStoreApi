using Domain.Dtos;
using Domain.Repositories;

namespace Domain.Services
{
    public class CostumerService : ICostumerService
    {
        private readonly ICostumerRepository _costumerRepository;
        public CostumerService(ICostumerRepository costumerRepository)
        {
            _costumerRepository = costumerRepository;
        }

        public Task CreateOrUpdateCostumerAsync(CreateUpdateCostumerDto request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
