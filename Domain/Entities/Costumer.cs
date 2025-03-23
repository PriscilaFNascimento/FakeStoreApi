using Domain.Dtos;

namespace Domain.Entities
{
    public class Costumer : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public Costumer(string name, string email)
        {
            Name = name;
            Email = email;
        }

        internal void UpdateName(string name)
        {
            Name = name;
        }

        public static CostumerResponseDto ToResponseDto(Costumer costumer)
        {
            return new CostumerResponseDto
            {
                Id = costumer.Id,
                Name = costumer.Name,
                Email = costumer.Email
            };
        }
    }
}
