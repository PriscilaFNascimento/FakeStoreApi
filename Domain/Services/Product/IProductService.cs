using Domain.Dtos;

namespace Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync(CancellationToken cancellationToken = default);
        Task<ProductResponseDto?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default);
    }
} 