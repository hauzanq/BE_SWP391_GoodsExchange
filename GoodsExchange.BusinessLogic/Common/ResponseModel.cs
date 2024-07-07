namespace GoodsExchange.BusinessLogic.Common
{
    public class ResponseModel<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public ResponseModel(string message, T data)
        {
            Message = message;
            Data = data;
        }
        public ResponseModel(T data)
        {
            Data = data;
        }
        public ResponseModel(string message)
        {
            Message = message;
        }
    }
}
