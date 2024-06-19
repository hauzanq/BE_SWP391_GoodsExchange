using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Email;
using MailKit.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.Services.Emails
{
    public interface IEmailServices
    {
        Task SendEmailToRegisterAsync(string to, string token);

        Task SendEmailToConfirmPasswordAsync(string to, string confirmPassword);

        Task SendEmailToNewPasswordAsync(string to, string confirmPassword);
    }

    public class EmailServices : IEmailServices
    {
        private readonly EmailSettings _emailSettings;
        private readonly string _webHostEnvironment;

        public EmailServices(IOptions<EmailSettings> options, IWebHostEnvironment webHostEnvironment)
        {
            _emailSettings = options.Value;
            _webHostEnvironment = webHostEnvironment.WebRootPath;
        }



        public async Task SendEmailToConfirmPasswordAsync(string to, string confirmPassword)
        {
            string content = EmailtemplateHelpers.CONFIRM_RESET_PASSWORD(_webHostEnvironment);
            var resetLink = $"http://localhost:5000/api/v1/users//Password/Reset/Confirm?email={to}&secret={confirmPassword}";

            content = content.Replace("{{Email}}", to);
            content = content.Replace("{{ResetPasswordUrl}}", resetLink);


            await SendMailAsync(to, "Reset password",content);
        }


        
        public Task SendEmailToNewPasswordAsync(string to, string confirmPassword)
        {
            throw new NotImplementedException();
        }

        public async Task SendEmailToRegisterAsync(string to, string token)
        {
            string content = EmailtemplateHelpers.REGISTER_TEMPLATE(_webHostEnvironment);
            var verificationLink = $"http://localhost:5000/api/v1/users/verifyemail?email={to}&token={token}";
            content = content.Replace("{{Email}}", to);
            content = content.Replace("{{token}}", verificationLink);


            await SendMailAsync(to, "Fgoodexchange send confirm Registration Account", content);

        
        }
        
        public async Task SendEmailToUpdatePasswordAsync(string to , string token)
        {
            string content= EmailtemplateHelpers.CONFIRM_RESET_PASSWORD(_webHostEnvironment);
            var resetPasswordLink = $"http://localhost:5000/api/v1/users/email={to}";
            content = content.Replace("{{Email}}", to);
            content = content.Replace("{{token}}", resetPasswordLink);

            await SendMailAsync(to, "Fgoodexchange send for confirm Reset Password of email", content);

        }


        private async Task SendMailAsync(string to, string subject, string content)
        {
            try
            {
                var Email = new MimeMessage();
                Email.Sender = MailboxAddress.Parse(_emailSettings.Email);

                Email.To.Add(MailboxAddress.Parse(to));
                var builder = new BodyBuilder();
                Email.Subject = subject;



                builder.HtmlBody = content;

                Email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);
                await smtp.SendAsync(Email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                // Log exception (e.g., using ILogger)
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw; // Re-throw or handle as necessary
            }

        }
    }
}
