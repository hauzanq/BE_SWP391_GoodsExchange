using GoodsExchange.BusinessLogic.ViewModels.PreOrder;

namespace GoodsExchange.BusinessLogic.ViewModels.Product
{
    public class UserProductDetailViewModel
    {
        public string ProductName { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public List<string> ProductImageUrl { get; set; }
        public string CategoryName { get; set; }
        public List<PreOrderViewModel> SentRequests { get; set; }
        public List<PreOrderViewModel> ReceivedRequests { get; set; }
    }
}
