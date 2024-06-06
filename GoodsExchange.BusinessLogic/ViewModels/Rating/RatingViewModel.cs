namespace GoodsExchange.BusinessLogic.ViewModels.Rating
{
    public class RatingViewModel
    {
        public Guid RatingId { get; set; }
        public int NumberStars { get; set; }
        public string Feedback { get; set; }
        public DateTime CreateDate { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
