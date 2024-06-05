namespace GoodsExchange.BusinessLogic.RequestModels.Report
{
    public class CreateReportRequestModel
    {
        public Guid ReceiverId { get; set; }
        public string Reason { get; set; }
        public Guid ProductId { get; set; }
    }
}
