using System.ComponentModel.DataAnnotations;

namespace GoodsExchange.BusinessLogic.RequestModels.Rating
{
    public class RatingsRequestModel
    {
        [Range(0, 5, ErrorMessage = "Min Stars must be between 0 and 5.")]
        public float? MinStars { get; set; } = 0;

        [Range(0, 5, ErrorMessage = "Max Stars must be between 0 and 5.")]
        public float? MaxStars { get; set; } = 5;
        public DateTime? FromDate { get; set; } = null;
        public DateTime? ToDate { get; set; } = null;
    }
}
