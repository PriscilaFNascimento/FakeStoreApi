namespace Domain.Entities
{
    public class OrderProduct
    {
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }

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
