namespace GoodsExchange.Data.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }
        public float Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime UploadDate { get; set; }
        public Guid UserUploadId { get; set; }
        public User UserUpload{ get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Rate Rate { get; set; }
        public List<Report> Reports { get; set; }
    }
}
