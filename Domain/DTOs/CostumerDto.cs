namespace Domain.Dtos
{
    public class CreateUpdateCostumerDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class CostumerResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
} 