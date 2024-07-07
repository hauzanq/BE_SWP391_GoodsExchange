using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly GoodsExchangeDbContext _context;

        public TransactionService(GoodsExchangeDbContext context)
        {
            _context = context;
        }

        public async Task CreateTransactionAsync(Guid preorderid)
        {
            var transaction = new Transaction()
            {
                PreOrderId = preorderid
            };

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
