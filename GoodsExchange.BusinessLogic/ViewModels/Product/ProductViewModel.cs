namespace GoodsExchange.BusinessLogic.ViewModels.Product
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }
        public float Price { get; set; }
        public bool IsActive { get; set; }
        public string UserUpload { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string CategoryName { get; set; }

    }
}
