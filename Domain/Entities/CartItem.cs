namespace Domain.Entities
{
    public class CartItem : BaseEntityWithBaseProduct
    {
        public Guid CostumerId { get; private set; }
        public virtual Costumer Costumer { get; set; }
        public int Quantity { get; private set; }

        public decimal TotalPrice => Quantity * ProductPrice;
        
        public CartItem(
            Guid costumerId,
            int productId,
            string productTitle,
            decimal productPrice,
            string productDescription,
            string productCategory,
            string productImage) : base(
                productId,
                productTitle,
                productPrice,
                productDescription,
                productCategory,
                productImage)
        {
            CostumerId = costumerId;
            Quantity = 1;
        }

        internal void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }
}
}
