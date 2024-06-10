namespace GoodsExchange.BusinessLogic.RequestModels.Report
{
    public class CreateReportRequestModel
    {
        public Guid ReportingUserId { get; set; }
        public Guid TargetUserId { get; set; }
        public string Reason { get; set; }
        public Guid ProductId { get; set; }
    }
}
