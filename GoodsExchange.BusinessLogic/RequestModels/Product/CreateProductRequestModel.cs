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

        [Required(ErrorMessage = "Category ID is required.")]
        public Guid CategoryId { get; set; }
    }
}
