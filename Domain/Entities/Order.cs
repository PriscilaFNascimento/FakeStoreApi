namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public int Number { get; set; }
        public Guid CostumerId { get; set; }
        public virtual Costumer Costumer { get; set; }

        public Order(Guid costumerId)
        {
            CostumerId = costumerId;
        }
    }
}
