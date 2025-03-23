using Domain.Dtos;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ProductService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("FakeStoreExternalApi");
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["FakeStoreExternalApi:ProductsEndpoint"]!);
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _httpClient.GetAsync("", cancellationToken);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync(cancellationToken);
                    return JsonSerializer.Deserialize<IEnumerable<ProductResponseDto>>(content);
                }

                return Enumerable.Empty<ProductResponseDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Failed to fetch products: {ex.Message}", ex);
            }
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/{id}", cancellationToken);
                
                if(response.IsSuccessStatusCode) 
                {
                    var content = await response.Content.ReadAsStringAsync(cancellationToken);
                    return JsonSerializer.Deserialize<ProductResponseDto>(content);
                }

                //TODO: Return a message to the client when product is not found
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Failed to fetch product with ID {id}: {ex.Message}", ex);
            }
        }
    }
} 