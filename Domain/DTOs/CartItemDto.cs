using Domain.Entities;

namespace Domain.Dtos
{
    public class CreateCartItemDto : BaseProductDto
    {
        public Guid CostumerId { get; set; }
    }

    public class UpdateCartItemDto
    {
        public int ProductQuantity { get; set; }
    }

    public class CartItemResponseDto : BaseProductDto
    {
        public Guid Id { get; set; }
        public Guid CostumerId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Quantity * ProductPrice;
    }
} 