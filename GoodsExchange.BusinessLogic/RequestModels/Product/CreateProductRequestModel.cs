using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GoodsExchange.BusinessLogic.RequestModels.Product
{
    public class CreateProductRequestModel
    {
        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public List<IFormFile> Images { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(1000, 1000000000, ErrorMessage = "Price must be between 1,000 VND and 1,000,000,000 VND.")]
        public float Price { get; set; }


        [Required(ErrorMessage = "Category ID is required.")]
        public Guid CategoryId { get; set; }
    }
}
