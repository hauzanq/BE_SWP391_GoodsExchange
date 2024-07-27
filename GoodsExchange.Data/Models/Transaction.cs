namespace GoodsExchange.Data.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public Guid ExchangeRequestId { get; set; }
        public ExchangeRequest ExchangeRequest { get; set; }
        public DateTime DateCreated { get; set; }
    }
}