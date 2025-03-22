namespace Domain.Dtos
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid CostumerId { get; set; }
        public string CostumerName { get; set; }
        public string CostumerEmail { get; set; }
    }
} 