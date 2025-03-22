namespace Domain.Dtos
{
    public class OrderProductResponseDto
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
} 