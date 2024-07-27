using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;

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
                ExchangeRequestId = preorderid
            };

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsProductInTransactionAsync(Guid productId)
        {
            return await _context.Transactions
                                 .Include(t => t.ExchangeRequest)
                                 .AnyAsync(t => t.ExchangeRequest.TargetProductId == productId);
        }
    }
}
