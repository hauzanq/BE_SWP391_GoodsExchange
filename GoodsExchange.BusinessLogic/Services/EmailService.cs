using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.Services
{
    public interface IEmailService
    {
        Task<ApiResult<bool>> SendEmailAsync(EmailRequestModel request);
    }
    public  class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> emailSetting) { 
            _emailSettings = emailSetting.Value;
        }

        public async Task<ApiResult<bool>> SendEmailAsync(EmailRequestModel emailRequest)
        {
            try
            {
                //Setup Email 
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_emailSettings.Email);
                email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
                email.Subject = emailRequest.Subject;

                var builder = new BodyBuilder();

                //Setup Email Body  
                builder.HtmlBody = emailRequest.Body;
                email.Body = builder.ToMessageBody();

                // Set up Identity with server smtp
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);
                // SEND MAIL 
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                return new ApiSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }
    }
}
