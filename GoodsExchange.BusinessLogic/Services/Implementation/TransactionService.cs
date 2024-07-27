using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Transaction;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.NetworkInformation;
using GoodsExchange.BusinessLogic.ViewModels.ExchangeRequest;

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
                ExchangeRequestId = preorderid,
                DateCreated = DateTime.Now,
            };

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<PageResult<TransactionViewModel>> GetTransactionsAsync(PagingRequestModel paging)
        {
            var query = _context.Transactions.Include(t => t.ExchangeRequest).AsQueryable();

            var data = query.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).Select(t => new TransactionViewModel()
            {
                TransactionId = t.TransactionId,
                ExchangeRequest = new ExchangeRequestViewModel()
                {
                    ExchangeRequestId = t.ExchangeRequest.ExchangeRequestId,

                    CurrentProductId = t.ExchangeRequest.CurrentProduct.ProductId,
                    CurrentProductName = t.ExchangeRequest.CurrentProduct.ProductName,
                    CurrentProductImage = t.ExchangeRequest.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).First(),

                    TargetProductId = t.ExchangeRequest.TargetProduct.ProductId,
                    TargetProductName = t.ExchangeRequest.TargetProduct.ProductName,
                    TargetProductImage = t.ExchangeRequest.TargetProduct.ProductImages.Select(pi => pi.ImagePath).First(),

                    ReceiverId = t.ExchangeRequest.Receiver.UserId,
                    ReceiverName = t.ExchangeRequest.Receiver.FirstName + " " + t.ExchangeRequest.Receiver.LastName,

                    SenderId = t.ExchangeRequest.Sender.UserId,
                    SenderName = t.ExchangeRequest.Sender.FirstName + " " + t.ExchangeRequest.Sender.LastName,

                    UserImage = t.ExchangeRequest.Receiver.UserImageUrl
                }

            }).ToListAsync();

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / paging.PageSize);

            return new PageResult<TransactionViewModel>()
            {
                CurrentPage = totalPages,
                Items = await data,
                TotalPage = totalPages,
            };
        }

        public async Task<bool> IsProductInTransactionAsync(Guid productId)
        {
            return await _context.Transactions
                                 .Include(t => t.ExchangeRequest)
                                 .AnyAsync(t => t.ExchangeRequest.TargetProductId == productId);
        }
    }
}
