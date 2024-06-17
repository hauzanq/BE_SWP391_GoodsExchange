namespace GoodsExchange.Data.Models
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public DateTime DateCreated { get; set; }
        public long FileSize { get; set; }
        public Product Product { get; set; }
    }
}
