namespace Domain.Entities
{
    public class Costumer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Costumer(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
