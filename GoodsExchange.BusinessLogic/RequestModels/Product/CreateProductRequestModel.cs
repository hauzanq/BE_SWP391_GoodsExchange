namespace GoodsExchange.BusinessLogic.RequestModels.Product
{
    public class CreateProductRequestModel
    {
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public string ProductImageUrl { get; set; }
        public float Price { get; set; }

        public Guid CategoryId { get; set; }
    }
}
