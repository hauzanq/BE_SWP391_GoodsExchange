namespace GoodsExchange.Data.Models
{
    public class ExchangeRequest
    {
        public Guid ExchangeRequestId { get; set; }

        public Guid CurrentProductId { get; set; }
        public Product CurrentProduct { get; set; }


        public Guid TargetProductId { get; set; }
        public Product TargetProduct { get; set; }

        public Guid SenderId { get; set; }
        public User Sender { get; set; }

        public Guid ReceiverId { get; set; }
        public User Receiver { get; set; }

        public string Status { get; set; }
        public int SenderStatus { get; set; }
        public int ReceiverStatus { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Transaction Transaction { get; set; }
    }
}
