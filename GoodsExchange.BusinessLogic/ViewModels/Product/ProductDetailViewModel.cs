namespace GoodsExchange.BusinessLogic.ViewModels.Product
{
    public class ProductDetailViewModel
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public List<string> ProductImageUrl { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Guid UserUploadId { get; set; }
        public string UserUpload { get; set; }
        public string UserImageUrl { get; set; }
        public int NumberOfRatings { get; set; }
        public float AverageNumberStars { get; set; }
        public string UserPhoneNumber { get; set; }
        public string CategoryName { get; set; }
    }
}
