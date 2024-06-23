namespace GoodsExchange.BusinessLogic.ViewModels.Rating
{
    public class RatingViewModel
    {
        public Guid RatingId { get; set; }
        public int NumberStars { get; set; }
        public string Feedback { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
