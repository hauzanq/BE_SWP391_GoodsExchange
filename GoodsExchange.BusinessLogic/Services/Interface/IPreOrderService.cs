using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.PreOrder;
using GoodsExchange.BusinessLogic.ViewModels.PreOrder;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IPreOrderService
    {
        Task<ResponseModel<PreOrderViewModel>> CreatePreOrderAsync(CreatePreOrderRequestModel request);
        Task<ResponseModel<List<PreOrderViewModel>>> GetPreOrdersForProductAsync(Guid productid);
        Task<ResponseModel<ConfirmPreOrderViewModel>> ConfirmPreOrderAsync(ConfirmPreOrderRequestModel request);
    }
}
