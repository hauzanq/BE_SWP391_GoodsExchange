namespace GoodsExchange.BusinessLogic.RequestModels.User
{
    public class UsersRequestModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
        public bool? Status { get; set; }
    }
}
