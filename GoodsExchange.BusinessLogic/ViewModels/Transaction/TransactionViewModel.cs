using GoodsExchange.BusinessLogic.ViewModels.ExchangeRequest;

namespace GoodsExchange.BusinessLogic.ViewModels.Transaction
{
    public class TransactionViewModel
    {
        public Guid TransactionId { get; set; }
        public ExchangeRequestViewModel ExchangeRequest { get; set; }
    }
}
