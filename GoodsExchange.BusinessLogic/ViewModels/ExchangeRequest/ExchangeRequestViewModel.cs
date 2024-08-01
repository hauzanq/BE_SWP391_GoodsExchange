namespace GoodsExchange.BusinessLogic.ViewModels.ExchangeRequest
{
    public class ExchangeRequestViewModel
    {
        public Guid ExchangeRequestId { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }

        public Guid ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public string UserImage { get; set; }

        public Guid CurrentProductId { get; set; }
        public string CurrentProductName { get; set; }
        public string CurrentProductImage { get; set; }
        public string CurrentProductDescription { get; set; }

        public Guid TargetProductId { get; set; }
        public string TargetProductName { get; set; }
        public string TargetProductImage { get; set; }
        public string TargetProductDescription { get; set; }

        public string Status { get; set; }
        public int SenderStatus { get; set; }
        public int ReceiverStatus { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
