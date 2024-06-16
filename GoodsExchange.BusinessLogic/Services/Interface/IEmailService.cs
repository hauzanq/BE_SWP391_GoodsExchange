using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Email;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IEmailService
    {
        Task<ApiResult<bool>> SendEmailAsync(EmailRequestModel request);
    }
}
