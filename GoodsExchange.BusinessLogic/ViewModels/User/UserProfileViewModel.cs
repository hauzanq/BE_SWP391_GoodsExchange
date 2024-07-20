namespace GoodsExchange.BusinessLogic.ViewModels.User
{
    public class UserProfileViewModel : BaseUserViewModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string UserImageUrl { get; set; }
        public float AverageNumberStars { get; set; }
    }
}
