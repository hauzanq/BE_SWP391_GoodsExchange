namespace GoodsExchange.BusinessLogic.Common
{
    public class ApiSuccessResult<T> : EntityResponse<T>
    {
        public ApiSuccessResult(T data)
        {
            Data = data;
        }
        public ApiSuccessResult()
        {
            Message = "Success";
        }
    }
}
