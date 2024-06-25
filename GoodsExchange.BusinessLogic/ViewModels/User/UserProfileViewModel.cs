namespace GoodsExchange.BusinessLogic.ViewModels.User
{
    public class UserProfileViewModel : BaseUserViewModel
    {
        public string UserName { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string UserImageUrl { get; set; }
    }
}
