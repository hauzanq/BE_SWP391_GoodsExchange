using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Email;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<ApiResult<bool>> SendEmailAsync(EmailRequestModel request)
        {
            throw new NotImplementedException();
        }
    }
}
