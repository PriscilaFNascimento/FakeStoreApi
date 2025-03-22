namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Number { get; set; }
        public Guid CostumerId { get; set; }
        public virtual Costumer Costumer { get; set; }

        public Order(string number, Guid costumerId)
        {
            Number = number;
            CostumerId = costumerId;
        }
    }
}
