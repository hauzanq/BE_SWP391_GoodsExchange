using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Email;
using GoodsExchange.BusinessLogic.Services.Interface;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Options;
using MimeKit;
using GoodsExchange.BusinessLogic.Common.Exceptions;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IEmailTemplateHelper _emailTemplateHelper;
        private readonly string _webHostEnvironment;
        private readonly IServer _server;
        public EmailService(IOptions<EmailSettings> emailSetting, IEmailTemplateHelper emailTemplateHelper, IWebHostEnvironment webHostEnvironment, IServer server)
        {
            _emailSettings = emailSetting.Value;
            _emailTemplateHelper = emailTemplateHelper;
            _webHostEnvironment = webHostEnvironment.WebRootPath;
            _server = server;
        }

        public async Task<EntityResponse<bool>> SendEmailAsync(string to, string subject, string content)
        {
            try
            {
                //Setup Email 
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_emailSettings.Email);
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;

                var builder = new BodyBuilder();

                //Setup Email Body  
                builder.HtmlBody = content;
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
                throw new BadRequestException(ex.Message);
            }
        }

        public async  Task SendEmailToRegisterAsync(string to, string token)
        {
            string content = _emailTemplateHelper.REGISTER_TEMPLATE(_webHostEnvironment);
            var serverAddress = _server.Features.Get<IServerAddressesFeature>().Addresses.First();
            //var verificationLink = $"http://localhost:5000/api/v1/users/verifyemail?email={to}&token={token}";
            var verificationLink = serverAddress + $"/api/v1/users/verify-email?email={to}&token={token}";
            content = content.Replace("{{Email}}", to);
            content = content.Replace("{{token}}", verificationLink);


            await SendEmailAsync(to, "Fgoodexchange send confirm Registration Account", content);

        }
    }
}
