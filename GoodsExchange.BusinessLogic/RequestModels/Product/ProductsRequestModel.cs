namespace GoodsExchange.BusinessLogic.RequestModels.Product
{
    public class ProductsRequestModel
    {
        public string? ProductName { get; set; }
        public DateTime? StartUploadDate { get; set; }
        public DateTime? EndUploadDate { get; set; }
        public string? CategoryName { get; set; }
    }
}
