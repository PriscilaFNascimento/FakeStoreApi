namespace Domain.Dtos
{
    public class CreateCartItemDto
    {
        public Guid CostumerId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }

    public class UpdateCartItemDto
    {
        public int ProductQuantity { get; set; }
    }

    public class CartItemResponseDto
    {
        public Guid CostumerId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalPrice => Quantity * ProductPrice;
    }
} 