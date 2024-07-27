namespace GoodsExchange.BusinessLogic.ViewModels.Product
{
    public class ProductListViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public List<string> ProductImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public bool IsReviewed { get; set; }
        public string UserUpload { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string CategoryName { get; set; }

    }
}
