using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
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
        private readonly IServiceWrapper _serviceWrapper;
        public TransactionService(GoodsExchangeDbContext context, IHttpContextAccessor httpContextAccessor, IServiceWrapper serviceWrapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceWrapper = serviceWrapper;
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
            var userid = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("id").Value);

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
                                .Where(t => (t.ExchangeRequest.SenderId == userid && t.ExchangeRequest.Status == SystemConstant.ExchangeRequestStatus.Complete)
                                    || (t.ExchangeRequest.ReceiverId == userid && t.ExchangeRequest.Status == SystemConstant.ExchangeRequestStatus.Complete))
                                .AsQueryable();

            var data = await query.Skip((paging.PageIndex - 1) * paging.PageSize)
                                  .Take(paging.PageSize)
                                  .Select(t => new TransactionViewModel()
                                  {
                                      TransactionId = t.TransactionId,
                                      ExchangeRequest = new ExchangeRequestViewModel()
                                      {
                                          ExchangeRequestId = t.ExchangeRequest.ExchangeRequestId,

                                          CurrentProductId = t.ExchangeRequest.SenderId == userid ? t.ExchangeRequest.CurrentProduct.ProductId : t.ExchangeRequest.TargetProduct.ProductId,
                                          CurrentProductName = t.ExchangeRequest.SenderId == userid ? t.ExchangeRequest.CurrentProduct.ProductName : t.ExchangeRequest.TargetProduct.ProductName,
                                          CurrentProductImage = t.ExchangeRequest.SenderId == userid ? t.ExchangeRequest.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).FirstOrDefault() : t.ExchangeRequest.TargetProduct.ProductImages.Select(pi => pi.ImagePath).FirstOrDefault(),
                                          CurrentProductDescription = t.ExchangeRequest.SenderId == userid ? t.ExchangeRequest.CurrentProduct.Description : t.ExchangeRequest.TargetProduct.Description,

                                          TargetProductId = t.ExchangeRequest.SenderId == userid ? t.ExchangeRequest.TargetProduct.ProductId : t.ExchangeRequest.CurrentProduct.ProductId,
                                          TargetProductName = t.ExchangeRequest.SenderId == userid ? t.ExchangeRequest.TargetProduct.ProductName : t.ExchangeRequest.CurrentProduct.ProductName,
                                          TargetProductImage = t.ExchangeRequest.SenderId == userid ? t.ExchangeRequest.TargetProduct.ProductImages.Select(pi => pi.ImagePath).FirstOrDefault() : t.ExchangeRequest.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).FirstOrDefault(),
                                          TargetProductDescription = t.ExchangeRequest.SenderId == userid ? t.ExchangeRequest.TargetProduct.Description : t.ExchangeRequest.CurrentProduct.Description,

                                          ReceiverId = t.ExchangeRequest.ReceiverId,
                                          ReceiverName = t.ExchangeRequest.Receiver.FirstName + " " + t.ExchangeRequest.Receiver.LastName,
                                          ReceiverStatus = t.ExchangeRequest.ReceiverStatus,

                                          SenderId = t.ExchangeRequest.SenderId,
                                          SenderName = t.ExchangeRequest.Sender.FirstName + " " + t.ExchangeRequest.Sender.LastName,
                                          SenderStatus = t.ExchangeRequest.SenderStatus,

                                          UserImage = t.ExchangeRequest.SenderId == userid ? t.ExchangeRequest.Receiver.UserImageUrl : t.ExchangeRequest.Sender.UserImageUrl,

                                          Status = t.ExchangeRequest.Status,

                                          StartTime = t.ExchangeRequest.StartTime,
                                          EndTime = t.ExchangeRequest.EndTime
                                      }
                                  })
                                  .ToListAsync();
            foreach (var transaction in data)
            {
                transaction.Rated = await _serviceWrapper.RatingServices.IsUserRatedOnProduct(userid, transaction.ExchangeRequest.TargetProductId);
                transaction.Reported = await _serviceWrapper.ReportServices.IsUserReportedOnProduct(userid, transaction.ExchangeRequest.TargetProductId);
            }

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
