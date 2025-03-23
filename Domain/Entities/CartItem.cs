namespace Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public Guid CostumerId { get; set; }
        public virtual Costumer Costumer { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }

        public decimal TotalPrice => Quantity * ProductPrice;

        public CartItem(Guid costumerId, string productName, decimal productPrice)
        {
            CostumerId = costumerId;
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = 1;
        }
    }
}
