namespace Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public Guid CostumerId { get; private set; }
        public virtual Costumer Costumer { get; set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal ProductPrice { get; private set; }

        public decimal TotalPrice => Quantity * ProductPrice;

        public CartItem(Guid costumerId, string productName, decimal productPrice)
        {
            CostumerId = costumerId;
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = 1;
        }

        internal void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
