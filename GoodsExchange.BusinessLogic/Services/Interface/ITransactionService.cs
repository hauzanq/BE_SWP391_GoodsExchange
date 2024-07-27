using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.ViewModels.Transaction;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface ITransactionService
    {
        Task CreateTransactionAsync(Guid preorderid);
        Task<PageResult<TransactionViewModel>> GetTransactionsAsync(PagingRequestModel model);
        Task<bool> IsProductInTransactionAsync(Guid productId);
    }
}
