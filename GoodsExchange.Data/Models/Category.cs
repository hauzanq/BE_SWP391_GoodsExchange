namespace GoodsExchange.Data.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
