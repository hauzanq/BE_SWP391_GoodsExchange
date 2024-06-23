namespace GoodsExchange.BusinessLogic.Common
{
    public class EntityResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
