namespace Domain.Entities
{
    public class OrderProduct : BaseProduct
    {
        public Guid OrderId { get; private set; }
        public virtual Order Order { get; set; }
        public int ProductQuantity { get; private set; }
        public decimal TotalPrice => ProductQuantity * ProductPrice;

        public OrderProduct(
            Guid orderId,
            int productQuantity,
            int productId,
            string productTitle,
            decimal productPrice,
            string productDescription,
            string productCategory,
            string productImage) : base(productId, productTitle, productPrice, productDescription, productCategory, productImage)
        {
            OrderId = orderId;
            ProductQuantity = productQuantity;
        }
    }
}
