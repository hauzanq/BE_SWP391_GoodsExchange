namespace GoodsExchange.BusinessLogic.ViewModels.Product
{
    public class ProductListViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public List<string> ProductImageUrl { get; set; }
        public int Status { get; set; }
        public string UserUpload { get; set; }
        public Guid UserUploadId { get; set; }
        public float AverageNumberStars { get; set; }
        public DateTime UploadDate { get; set; }
        public string CategoryName { get; set; }
    }
}
