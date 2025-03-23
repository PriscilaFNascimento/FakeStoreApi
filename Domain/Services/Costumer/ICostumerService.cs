using Domain.Dtos;

namespace Domain.Services
{
    public interface ICostumerService
    {
        Task<CostumerResponseDto> CreateOrUpdateCostumerAsync(CreateUpdateCostumerDto request, CancellationToken cancellationToken);
    }
}
