using System.ComponentModel.DataAnnotations;

namespace GoodsExchange.BusinessLogic.RequestModels.Report
{
    public class CreateReportRequestModel
    {
        [Required(ErrorMessage = "Reason is required.")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Product ID is required.")]
        public Guid ProductId { get; set; }
    }
}
