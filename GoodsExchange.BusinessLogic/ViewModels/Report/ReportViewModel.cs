namespace GoodsExchange.BusinessLogic.ViewModels.Report
{
    public class ReportViewModel
    {
        public Guid ReportId { get; set; }
        public string ReportMade { get; set; }
        public string ReportReceived { get; set; }
        public string Reason { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public bool IsApprove { get; set; }
        public bool IsActive { get; set; }
    }
}
