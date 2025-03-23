namespace Domain.Entities
{
    public class OrderProduct
    {
        public Guid OrderId { get; private set; }
        public virtual Order Order { get; set; }
        public string ProductName { get; private set; }
        public int ProductQuantity { get; private set; }
        public decimal ProductPrice { get; private set; }

        public decimal TotalPrice => ProductQuantity * ProductPrice;

        public OrderProduct(Guid orderId, string productName, int productQuantity, decimal productPrice)
        {
            OrderId = orderId;
            ProductName = productName;
            ProductQuantity = productQuantity;
            ProductPrice = productPrice;
        }
    }
}
