using System.ComponentModel.DataAnnotations;

namespace GoodsExchange.BusinessLogic.RequestModels.Rating
{
    public class CreateRatingRequestModel
    {
        [Required(ErrorMessage = "Number of stars is required.")]
        [Range(1, 5, ErrorMessage = "Number of stars must be between 1 and 5.")]
        public int NumberStars { get; set; }


        public string Feedback { get; set; }

        [Required(ErrorMessage = "Product ID is required.")]
        public Guid ProductId { get; set; }
    }
}
