using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Common.Exceptions;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.ExchangeRequest;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IServiceWrapper _serviceWrapper;
        private readonly string _webHostEnvironment;
        private readonly IServer _server;

        public EmailService(IOptions<EmailSettings> emailSettings, IServiceWrapper serviceWrapper, IWebHostEnvironment webHostEnvironment, IServer server)
        {
            _emailSettings = emailSettings.Value;
            _serviceWrapper = serviceWrapper;
            _webHostEnvironment = webHostEnvironment.WebRootPath;
            _server = server;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string content)
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

                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task SendEmailToRegisterAsync(string to, string token)
        {
            string content = _serviceWrapper.EmailHelperServices.REGISTER_TEMPLATE(_webHostEnvironment);
            var serverAddress = _server.Features.Get<IServerAddressesFeature>().Addresses.First();
            //var verificationLink = $"http://localhost:5000/api/v1/users/verifyemail?email={to}&token={token}";
            var verificationLink = serverAddress + $"/api/v1/users/verify-email?email={to}&token={token}";
            content = content.Replace("{{Email}}", to);
            content = content.Replace("{{token}}", verificationLink);


            await SendEmailAsync(to, "Fgoodexchange send confirm Registration Account", content);

        }

        public async Task SendEmailToUpdateProfile(string to, string token)
        {
            string content = _serviceWrapper.EmailHelperServices.UPDATE_NEWEMAIL_TEMPLATE(_webHostEnvironment);
            var serverAddress = _server.Features.Get<IServerAddressesFeature>().Addresses.First();
            var verificationLink = serverAddress + $"/api/v1/users/verify-email?email={to}&token={token}";
            content = content.Replace("{{Email}}", to);
            content = content.Replace("{{token}}", verificationLink);
            bool result = await SendEmailAsync(to, "Fgoodexchange send confirm Update Profile Account", content);



        }

        public async Task SendMailForExchangeRequest(string to, ExchangeRequestViewModel model)
        {
            string content = _serviceWrapper.EmailHelperServices.EXCHANGE_REQUEST_TEMPLATE(_webHostEnvironment);

            // Replace placeholders with actual data
            content = content.Replace("{{ReceiverName}}", model.ReceiverName)
                             .Replace("{{SenderName}}", model.SenderName)
                             .Replace("{{CurrentProductName}}", model.CurrentProductName)
                             .Replace("{{CurrentProductImage}}", model.CurrentProductImage)
                             .Replace("{{TargetProductName}}", model.TargetProductName)
                             .Replace("{{TargetProductImage}}", model.TargetProductImage)
                             .Replace("{{ExchangeRequestId}}", model.ExchangeRequestId.ToString());

            // Send the email
            await SendEmailAsync(to, "Exchange Request Notification", content);
        }
    }
}

