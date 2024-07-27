namespace GoodsExchange.BusinessLogic.RequestModels.ExchangeRequest
{
    public class CreateExchangeRequestModel
    {
        public Guid CurrentProductId { get; set; }
        public Guid TargetProductId { get; set; }
    }
}
