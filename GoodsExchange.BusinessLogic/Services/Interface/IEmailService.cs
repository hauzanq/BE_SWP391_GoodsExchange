namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string content);
        Task SendEmailToRegisterAsync(string to, string token);
        Task SendEmailToUpdateProfile(string to, string token);
    }
}
