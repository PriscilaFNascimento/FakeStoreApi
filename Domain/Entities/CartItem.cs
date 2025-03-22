﻿namespace Domain.Entities
{
    public class CartItem
    {
        public Guid CostumerId { get; set; }
        public virtual Costumer Costumer { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }

        public decimal TotalPrice => ProductQuantity * ProductPrice;

        public CartItem(Guid costumerId, string productName, decimal productPrice)
        {
            CostumerId = costumerId;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductQuantity = 1;
        }
    }
}
