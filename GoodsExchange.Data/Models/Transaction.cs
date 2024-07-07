namespace GoodsExchange.Data.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public Guid PreOrderId { get; set; }
        public PreOrder PreOrder { get; set; }
    }
}