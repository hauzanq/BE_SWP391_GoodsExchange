namespace GoodsExchange.Data.Models
{
    public class PreOrder
    {
        public Guid PreOrderId { get; set; }

        public Guid CurrentProductId { get; set; }
        public Product CurrentProduct { get; set; }


        public Guid TargetProductId { get; set; }
        public Product TargetProduct { get; set; }

        public Guid BuyerId { get; set; }
        public User Buyer { get; set; }

        public Guid SellerId { get; set; }
        public User Seller { get; set; }

        public bool Status { get; set; }
        public bool BuyerConfirmed { get; set; }
        public bool SellerConfirmed { get; set; }
        public DateTime DateCreated { get; set; }
        public Transaction Transaction { get; set; }
    }
}
