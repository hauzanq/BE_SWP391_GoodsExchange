using GoodsExchange.BusinessLogic.ViewModels.ExchangeRequest;

namespace GoodsExchange.BusinessLogic.ViewModels.Product
{
    public class UserProductDetailViewModel
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public List<string> ProductImageUrl { get; set; }
        public string CategoryName { get; set; }
        public List<ExchangeRequestViewModel> SentRequests { get; set; }
        public List<ExchangeRequestViewModel> ReceivedRequests { get; set; }
    }
}
