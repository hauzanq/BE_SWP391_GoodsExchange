using GoodsExchange.BusinessLogic.ViewModels.ExchangeRequest;

namespace GoodsExchange.BusinessLogic.ViewModels.Transaction
{
    public class TransactionViewModel
    {
        public Guid TransactionId { get; set; }
        public bool Rated { get; set; }
        public bool Reported { get; set; }
        public ExchangeRequestViewModel ExchangeRequest { get; set; }
    }
}
