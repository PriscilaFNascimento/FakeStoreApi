namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdatedAt { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            LastUpdatedAt = DateTime.UtcNow;
        }
    }

    public abstract class BaseProduct
    {
        public int ProductId { get; private set; }
        public string ProductTitle { get; private set; }
        public decimal ProductPrice { get; private set; }
        public string ProductDescription { get; private set; }
        public string ProductCategory { get; private set; }
        public string ProductImage { get; private set; }

        protected BaseProduct()
        {
                
        }

        protected BaseProduct(
            int productId,
            string productTitle,
            decimal productPrice,
            string productDescription,
            string productCategory,
            string productImage)
        {
            ProductId = productId;
            ProductTitle = productTitle;
            ProductPrice = productPrice;
            ProductDescription = productDescription;
            ProductCategory = productCategory;
            ProductImage = productImage;
        }
    }

    public abstract class BaseEntityWithBaseProduct : BaseEntity
    {
        public int ProductId { get; protected set; }
        public string ProductTitle { get; protected set; }
        public decimal ProductPrice { get; protected set; }
        public string ProductDescription { get; protected set; }
        public string ProductCategory { get; protected set; }
        public string ProductImage { get; protected set; }

        protected BaseEntityWithBaseProduct(
            int productId,
            string productTitle,
            decimal productPrice,
            string productDescription,
            string productCategory,
            string productImage)
        {
            ProductId = productId;
            ProductTitle = productTitle;
            ProductPrice = productPrice;
            ProductDescription = productDescription;
            ProductCategory = productCategory;
            ProductImage = productImage;
        }
    }
}
