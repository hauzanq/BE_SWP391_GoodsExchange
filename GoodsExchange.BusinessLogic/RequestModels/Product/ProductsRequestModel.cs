namespace GoodsExchange.BusinessLogic.RequestModels.Product
{
    public class ProductsRequestModel
    {
        public string? ProductName { get; set; }

        public DateTime? StartUploadDate { get; set; }
        public DateTime? EndUploadDate { get; set; }
        public DateTime? StartApprovedDate { get; set; }
        public bool? IsApproved { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? EndApprovedDate { get; set; }
        public string? CategoryName { get; set; }
    }
}
