namespace GoodsExchange.BusinessLogic.RequestModels.Rating
{
    public class CreateRatingRequestModel
    {
        public Guid ReceiverId { get; set; }
        public int NumberStars { get; set; }
        public string Feedback { get; set; }
        public Guid ProductId { get; set; }
    }
}
