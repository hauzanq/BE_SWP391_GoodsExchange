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

        [Required(ErrorMessage = "Price is required.")]
        [Range(1000, 1000000000, ErrorMessage = "Price must be between 1,000 VND and 1,000,000,000 VND.")]
        public float? Price { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
