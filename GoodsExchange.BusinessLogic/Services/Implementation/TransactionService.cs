using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.ExchangeRequest;
using GoodsExchange.BusinessLogic.ViewModels.Transaction;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionService(GoodsExchangeDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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
            var userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("id").Value);

            var query = _context.Transactions
                                .Include(t => t.ExchangeRequest)
                                .ThenInclude(er => er.CurrentProduct)
                                .ThenInclude(cp => cp.ProductImages)
                                .Include(t => t.ExchangeRequest)
                                .ThenInclude(er => er.TargetProduct)
                                .ThenInclude(tp => tp.ProductImages)
                                .Include(t => t.ExchangeRequest)
                                .ThenInclude(er => er.Sender)
                                .Include(t => t.ExchangeRequest)
                                .ThenInclude(er => er.Receiver)
                                .AsQueryable();

            var data = await query.Skip((paging.PageIndex - 1) * paging.PageSize)
                                  .Take(paging.PageSize)
                                  .Select(t => new TransactionViewModel()
                                  {
                                      TransactionId = t.TransactionId,
                                      ExchangeRequest = new ExchangeRequestViewModel()
                                      {
                                          ExchangeRequestId = t.ExchangeRequest.ExchangeRequestId,

                                          // Determine current and target based on user role in the transaction
                                          CurrentProductId = t.ExchangeRequest.SenderId == userId ? t.ExchangeRequest.CurrentProduct.ProductId : t.ExchangeRequest.TargetProduct.ProductId,
                                          CurrentProductName = t.ExchangeRequest.SenderId == userId ? t.ExchangeRequest.CurrentProduct.ProductName : t.ExchangeRequest.TargetProduct.ProductName,
                                          CurrentProductImage = t.ExchangeRequest.SenderId == userId ? t.ExchangeRequest.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).FirstOrDefault() : t.ExchangeRequest.TargetProduct.ProductImages.Select(pi => pi.ImagePath).FirstOrDefault(),
                                          CurrentProductDescription = t.ExchangeRequest.SenderId == userId ? t.ExchangeRequest.CurrentProduct.Description : t.ExchangeRequest.TargetProduct.Description,

                                          TargetProductId = t.ExchangeRequest.SenderId == userId ? t.ExchangeRequest.TargetProduct.ProductId : t.ExchangeRequest.CurrentProduct.ProductId,
                                          TargetProductName = t.ExchangeRequest.SenderId == userId ? t.ExchangeRequest.TargetProduct.ProductName : t.ExchangeRequest.CurrentProduct.ProductName,
                                          TargetProductImage = t.ExchangeRequest.SenderId == userId ? t.ExchangeRequest.TargetProduct.ProductImages.Select(pi => pi.ImagePath).FirstOrDefault() : t.ExchangeRequest.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).FirstOrDefault(),
                                          TargetProductDescription = t.ExchangeRequest.SenderId == userId ? t.ExchangeRequest.TargetProduct.Description : t.ExchangeRequest.CurrentProduct.Description,

                                          // Determine user roles
                                          ReceiverId = t.ExchangeRequest.ReceiverId,
                                          ReceiverName = t.ExchangeRequest.Receiver.FirstName + " " + t.ExchangeRequest.Receiver.LastName,
                                          ReceiverStatus = t.ExchangeRequest.ReceiverStatus,

                                          SenderId = t.ExchangeRequest.SenderId,
                                          SenderName = t.ExchangeRequest.Sender.FirstName + " " + t.ExchangeRequest.Sender.LastName,
                                          SenderStatus = t.ExchangeRequest.SenderStatus,

                                          // Determine user image
                                          UserImage = t.ExchangeRequest.SenderId == userId ? t.ExchangeRequest.Receiver.UserImageUrl : t.ExchangeRequest.Sender.UserImageUrl,

                                          Status = t.ExchangeRequest.Status,

                                          StartTime = t.ExchangeRequest.StartTime,
                                          EndTime = t.ExchangeRequest.EndTime
                                      }
                                  })
                                  .ToListAsync();

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / paging.PageSize);

            return new PageResult<TransactionViewModel>()
            {
                CurrentPage = paging.PageIndex,
                Items = data,
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
