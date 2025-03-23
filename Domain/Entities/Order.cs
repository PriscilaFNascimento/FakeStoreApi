namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public int Number { get; }
        public Guid CostumerId { get; private set; }
        public virtual Costumer Costumer { get; set; }

        public Order(Guid costumerId)
        {
            CostumerId = costumerId;
        }
    }
}
