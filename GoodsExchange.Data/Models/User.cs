namespace GoodsExchange.Data.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirm { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateCreated { get; set; }
        public string PhoneNumber { get; set; }
        public string UserImageUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public List<Rating> RatingsGiven { get; set; }
        public List<Rating> RatingsReceived { get; set; }
        public List<Report> ReportsMade { get; set; }
        public List<Report> ReportsReceived { get; set; }
        public List<Product> Products { get; set; }
        public List<ExchangeRequest> PreOrderToBuyers { get; set; }
        public List<ExchangeRequest> PreOrderToSellers { get; set; }
    }
}
