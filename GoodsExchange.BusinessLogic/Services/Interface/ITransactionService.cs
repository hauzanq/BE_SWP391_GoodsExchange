namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface ITransactionService
    {
        Task CreateTransactionAsync(Guid preorderid);
    }
}
