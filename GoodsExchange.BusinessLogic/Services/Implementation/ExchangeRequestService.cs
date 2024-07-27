using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Common.Exceptions;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.Extensions;
using GoodsExchange.BusinessLogic.RequestModels.ExchangeRequest;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.ExchangeRequest;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class ExchangeRequestService : IExchangeRequestService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceWrapper _serviceWrapper;
        public ExchangeRequestService(GoodsExchangeDbContext context, IHttpContextAccessor httpContextAccessor, IServiceWrapper serviceWrapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceWrapper = serviceWrapper;
        }

        private async Task<string> GetStatusOfExchangeRequest(ExchangeRequest request)
        {
            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            if (user.UserId == request.SenderId)
            {
                request.SenderStatus += 1;
            }
            else
            {
                request.ReceiverStatus += 1;
            }

            if (request.SenderStatus == 1)
            {
                request.Status = SystemConstant.ExchangeRequestStatus.Created;
            }

            if (request.SenderStatus == 1 && request.ReceiverStatus == 1)
            {
                request.Status = SystemConstant.ExchangeRequestStatus.Approved;
            }

            if (request.SenderStatus == 2 && request.ReceiverStatus == 2)
            {
                request.Status = SystemConstant.ExchangeRequestStatus.Complete;
            }

            await _context.SaveChangesAsync();

            return request.Status;
        }

        public async Task<ResponseModel<ExchangeRequestViewModel>> ConfirmExchangeAsync(Guid requestid)
        {
            var request = await _context.ExchangeRequests.Include(ex => ex.CurrentProduct)
                                                .Include(ex => ex.TargetProduct)
                                                .Include(ex => ex.Sender)
                                                .Include(ex => ex.Receiver)
                                                .Where(ex => ex.ExchangeRequestId == requestid).FirstOrDefaultAsync();
            if (request == null)
            {
                throw new NotFoundException("This request does not exist.");
            }

            var status = await GetStatusOfExchangeRequest(request);

            if (status == SystemConstant.ExchangeRequestStatus.Complete)
            {
                await _serviceWrapper.TransactionService.CreateTransactionAsync(request.ExchangeRequestId);
            }

            return new ResponseModel<ExchangeRequestViewModel>("Confirm exchange successfully.");
        }

        public async Task<ResponseModel<ExchangeRequestViewModel>> SendExchangeRequestAsync(CreateExchangeRequestModel request)
        {
            var currentProduct = await _serviceWrapper.ProductServices.GetProductAsync(request.CurrentProductId);
            if (currentProduct == null)
            {
                throw new NotFoundException("The product does not exist.");
            }

            var targetProduct = await _serviceWrapper.ProductServices.GetProductAsync(request.TargetProductId);
            if (targetProduct == null)
            {
                throw new NotFoundException("The product does not exist.");
            }

            var sender = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            var receiver = await _serviceWrapper.UserServices.GetUserByProductId(targetProduct.ProductId);

            var exchange = new ExchangeRequest()
            {
                SenderId = sender.UserId,
                SenderStatus = 1,
                CurrentProductId = currentProduct.ProductId,
                ReceiverId = receiver.UserId,
                ReceiverStatus = 0,
                TargetProductId = targetProduct.ProductId,
                DateCreated = DateTime.Now,
                Status = SystemConstant.ExchangeRequestStatus.Created
            };

            await _context.ExchangeRequests.AddAsync(exchange);
            await _context.SaveChangesAsync();

            var data = new ExchangeRequestViewModel()
            {
                ExchangeRequestId = exchange.ExchangeRequestId,

                CurrentProductId = currentProduct.ProductId,
                CurrentProductName = currentProduct.ProductName,
                CurrentProductImage = currentProduct.ProductImages.Select(pi => pi.ImagePath).First(),

                TargetProductId = targetProduct.ProductId,
                TargetProductName = targetProduct.ProductName,
                TargetProductImage = targetProduct.ProductImages.Select(pi => pi.ImagePath).First(),

                ReceiverId = receiver.UserId,
                ReceiverName = receiver.FirstName + " " + receiver.LastName,

                SenderId = sender.UserId,
                SenderName = sender.FirstName + " " + sender.LastName,

                UserImage = receiver.UserImageUrl
            };

            return new ResponseModel<ExchangeRequestViewModel>("The exchange request was created successfully.", data);
        }

        public async Task<List<ExchangeRequestViewModel>> GetReceivedExchangeRequestsAsync()
        {
            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var result = await _context.ExchangeRequests
               .Where(ex => ex.ReceiverId == user.UserId)
               .Include(ex => ex.CurrentProduct)
               .Include(ex => ex.TargetProduct)
               .Include(ex => ex.Sender)
               .Include(ex => ex.Receiver)
               .Select(ex => new ExchangeRequestViewModel
               {
                   ExchangeRequestId = ex.ExchangeRequestId,

                   CurrentProductId = ex.CurrentProduct.ProductId,
                   CurrentProductName = ex.CurrentProduct.ProductName,
                   CurrentProductImage = ex.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).First(),

                   TargetProductId = ex.TargetProduct.ProductId,
                   TargetProductName = ex.TargetProduct.ProductName,
                   TargetProductImage = ex.TargetProduct.ProductImages.Select(pi => pi.ImagePath).First(),

                   ReceiverId = ex.Receiver.UserId,
                   ReceiverName = ex.Receiver.FirstName + " " + ex.Receiver.LastName,

                   SenderId = ex.Sender.UserId,
                   SenderName = ex.Sender.FirstName + " " + ex.Sender.LastName,

                   UserImage = ex.Sender.UserImageUrl
               })
               .ToListAsync();

            return result;
        }

        public async Task<List<ExchangeRequestViewModel>> GetSentExchangeRequestsAsync()
        {
            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var result = await _context.ExchangeRequests
               .Where(ex => ex.SenderId == user.UserId)
               .Include(ex => ex.CurrentProduct)
               .Include(ex => ex.TargetProduct)
               .Include(ex => ex.Sender)
               .Include(ex => ex.Receiver)
               .Select(ex => new ExchangeRequestViewModel
               {
                   ExchangeRequestId = ex.ExchangeRequestId,

                   CurrentProductId = ex.CurrentProduct.ProductId,
                   CurrentProductName = ex.CurrentProduct.ProductName,
                   CurrentProductImage = ex.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).First(),

                   TargetProductId = ex.TargetProduct.ProductId,
                   TargetProductName = ex.TargetProduct.ProductName,
                   TargetProductImage = ex.TargetProduct.ProductImages.Select(pi => pi.ImagePath).First(),

                   ReceiverId = ex.Receiver.UserId,
                   ReceiverName = ex.Receiver.FirstName + " " + ex.Receiver.LastName,

                   SenderId = ex.Sender.UserId,
                   SenderName = ex.Sender.FirstName + " " + ex.Sender.LastName,

                   UserImage = ex.Receiver.UserImageUrl
               })
               .ToListAsync();

            return result;
        }

        public async Task<bool> IsProductInExchangeProcessingAsync(Guid productId)
        {
            return await _context.ExchangeRequests.AnyAsync(ex => ex.TargetProductId == productId);
        }
    }
}
