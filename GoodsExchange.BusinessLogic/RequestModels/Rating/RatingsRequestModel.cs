using MailKit.Search;
using System.ComponentModel.DataAnnotations;

namespace GoodsExchange.BusinessLogic.RequestModels.Rating
{
    public class RatingsRequestModel
    {
        [Range(0, 5, ErrorMessage = "Min Stars must be between 0 and 5.")]
        public float? MinStars { get; set; }

        [Range(0, 5, ErrorMessage = "Max Stars must be between 0 and 5.")]
        public float? MaxStars { get; set; }
        public string? FeedbackSearchTerm { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? SenderId { get; set; }
        public Guid? ProductId { get; set; }
    }
}
