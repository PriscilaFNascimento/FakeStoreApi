using Domain.Dtos;

namespace Domain.Services
{
    public interface ICostumerService
    {
        Task CreateOrUpdateCostumerAsync(CreateUpdateCostumerDto request, CancellationToken cancellationToken = default);
    }
}
