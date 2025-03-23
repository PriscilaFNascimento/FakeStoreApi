namespace Domain.Dtos
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalProductsQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 