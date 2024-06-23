using MailKit.Search;

namespace GoodsExchange.BusinessLogic.RequestModels.Rating
{
    public class RatingsRequestModel
    {
        public float? MinStars { get; set; }
        public float? MaxStars { get; set; }
        public string? FeedbackSearchTerm { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? SenderId { get; set; }
        public Guid? ProductId { get; set; }
    }
}
