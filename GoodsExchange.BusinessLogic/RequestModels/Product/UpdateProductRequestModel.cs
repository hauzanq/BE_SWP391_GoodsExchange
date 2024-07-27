using System.ComponentModel.DataAnnotations;

namespace GoodsExchange.BusinessLogic.RequestModels.Product
{
    public class UpdateProductRequestModel
    {
        [Required(ErrorMessage = "Product ID is required.")]
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? ProductImageUrl { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
