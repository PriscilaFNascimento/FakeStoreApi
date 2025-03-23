using Domain.Entities;

namespace Domain.Dtos
{
    public class OrderProductResponseDto : BaseProductDto
    {
        public Guid OrderId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal TotalPrice => ProductQuantity * ProductPrice;
    }
} 