namespace GoodsExchange.Data.Models
{
    public class Rating
    {
        public Guid RatingId { get; set; }
        public int NumberStars { get; set; }
        public string Feedback { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public User Receiver { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
