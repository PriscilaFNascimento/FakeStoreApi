namespace Domain.Dtos
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal Total { get; set; }
    }
} 