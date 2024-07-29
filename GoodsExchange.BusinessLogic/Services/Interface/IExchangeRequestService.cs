using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.ExchangeRequest;
using GoodsExchange.BusinessLogic.ViewModels.ExchangeRequest;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IExchangeRequestService
    {
        Task<ResponseModel<ExchangeRequestViewModel>> SendExchangeRequestAsync(CreateExchangeRequestModel request);
        Task<ResponseModel<ExchangeRequestViewModel>> ConfirmExchangeAsync(Guid requestid);
        Task<ResponseModel<ExchangeRequestViewModel>> DenyExchangeAsync(Guid requestid);
        Task<List<ExchangeRequestViewModel>> GetSentExchangeRequestsAsync();
        Task<List<ExchangeRequestViewModel>> GetReceivedExchangeRequestsAsync();
        Task<List<ExchangeRequestViewModel>> GetStatusExchangeRequestsAsync();
        Task<bool> IsProductInExchangeProcessingAsync(Guid productId);
    }
}
