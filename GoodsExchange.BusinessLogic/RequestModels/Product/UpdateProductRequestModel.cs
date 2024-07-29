using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GoodsExchange.BusinessLogic.RequestModels.Product
{
    public class UpdateProductRequestModel
    {
        [Required(ErrorMessage = "Product ID is required.")]
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public List<IFormFile> Images { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
