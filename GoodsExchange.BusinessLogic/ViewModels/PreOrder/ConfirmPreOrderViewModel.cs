namespace GoodsExchange.BusinessLogic.ViewModels.PreOrder
{
    public class ConfirmPreOrderViewModel
    {
        public string ProductName { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }
        public bool BuyerConfirmed { get; set; }
        public bool SellerConfirmed { get; set; }
    }
}
