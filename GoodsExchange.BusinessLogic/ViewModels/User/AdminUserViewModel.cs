namespace GoodsExchange.BusinessLogic.ViewModels.User
{
    public class AdminUserViewModel : BaseUserViewModel
    {
        public Guid UserId { get; set; }
        public string RoleName { get; set; }
        public bool Status { get; set; }
        public float AverageNumberStars { get; set; }
        public int NumberReports { get; set; }
    }
}
