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

            if (_context.ExchangeRequests.Any(ex => ex.TargetProductId == request.TargetProductId))
            {
                throw new BadRequestException("The request to exchange this product have created.");
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
                ReceiverStatus = exchange.ReceiverStatus,

                SenderId = sender.UserId,
                SenderName = sender.FirstName + " " + sender.LastName,
                SenderStatus = exchange.SenderStatus,

                UserImage = sender.UserImageUrl,

                Status = exchange.Status
            };

            await _serviceWrapper.EmailServices.SendMailForExchangeRequest(receiver.Email, data);

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

                   CurrentProductId = ex.TargetProduct.ProductId,
                   CurrentProductName = ex.TargetProduct.ProductName,
                   CurrentProductImage = ex.TargetProduct.ProductImages.Select(pi => pi.ImagePath).First(),

                   TargetProductId = ex.CurrentProduct.ProductId,
                   TargetProductName = ex.CurrentProduct.ProductName,
                   TargetProductImage = ex.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).First(),

                   ReceiverId = ex.Receiver.UserId,
                   ReceiverName = ex.Receiver.FirstName + " " + ex.Receiver.LastName,
                   ReceiverStatus = ex.ReceiverStatus,

                   SenderId = ex.Sender.UserId,
                   SenderName = ex.Sender.FirstName + " " + ex.Sender.LastName,
                   SenderStatus = ex.SenderStatus,

                   UserImage = ex.Sender.UserImageUrl,

                   Status = ex.Status
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
                   ReceiverStatus = ex.ReceiverStatus,

                   SenderId = ex.Sender.UserId,
                   SenderName = ex.Sender.FirstName + " " + ex.Sender.LastName,
                   SenderStatus = ex.SenderStatus,

                   UserImage = ex.Receiver.UserImageUrl,

                   Status = ex.Status
               })
               .ToListAsync();

            return result;
        }

        public async Task<bool> IsProductInExchangeProcessingAsync(Guid productId)
        {
            return await _context.ExchangeRequests.AnyAsync(ex => ex.TargetProductId == productId);
        }
        private async Task<string> GetStatusOfExchangeRequest(ExchangeRequest request)
        {
            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            if (user.UserId == request.SenderId)
            {
                if (request.ReceiverStatus != 0)
                {
                    if (request.SenderStatus < 2)
                    {
                        request.SenderStatus += 1;
                    }
                    else
                    {
                        throw new BadRequestException("No more next status.");
                    }
                }
                else
                {
                    throw new BadRequestException("Waiting for sender approve this request.");
                }
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
                request.CurrentProduct.IsActive = false;
                request.TargetProduct.IsActive = false;
                request.Status = SystemConstant.ExchangeRequestStatus.Approved;
            }

            if ((request.SenderStatus == 2 && request.ReceiverStatus == 1) || (request.SenderStatus == 1 && request.ReceiverStatus == 2))
            {
                request.CurrentProduct.IsActive = false;
                request.TargetProduct.IsActive = false;
                request.Status = SystemConstant.ExchangeRequestStatus.Approved;
            }

            if (request.SenderStatus == 2 && request.ReceiverStatus == 2)
            {
                request.CurrentProduct.IsActive = false;
                request.TargetProduct.IsActive = false;
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

            request.DateCreated = DateTime.Now;

            if (status == SystemConstant.ExchangeRequestStatus.Complete)
            {
                await _serviceWrapper.TransactionService.CreateTransactionAsync(request.ExchangeRequestId);
            }

            return new ResponseModel<ExchangeRequestViewModel>("Confirm exchange successfully.");
        }
        public async Task<ResponseModel<ExchangeRequestViewModel>> DenyExchangeAsync(Guid requestid)
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
            // Change status for exhchange request
            request.Status = SystemConstant.ExchangeRequestStatus.Cancelled;

            // Enable product while deny exchange request
            request.CurrentProduct.IsActive = true;
            request.TargetProduct.IsActive = true;

            await _context.SaveChangesAsync();

            return new ResponseModel<ExchangeRequestViewModel>("Deny exchange successfully.");
        }

        public async Task<List<ExchangeRequestViewModel>> GetStatusExchangeRequestsAsync()
        {
            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var result = await _context.ExchangeRequests
                .Where(ex => ex.SenderId == user.UserId || ex.ReceiverId == user.UserId)
                .Include(ex => ex.CurrentProduct)
                .Include(ex => ex.TargetProduct)
                .Include(ex => ex.Sender)
                .Include(ex => ex.Receiver)
                .Select(ex => new ExchangeRequestViewModel
                {
                    ExchangeRequestId = ex.ExchangeRequestId,

                    CurrentProductId = user.UserId == ex.SenderId ? ex.CurrentProduct.ProductId : ex.TargetProduct.ProductId,
                    CurrentProductName = user.UserId == ex.SenderId ? ex.CurrentProduct.ProductName : ex.TargetProduct.ProductName,
                    CurrentProductImage = user.UserId == ex.SenderId ? ex.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).First() : ex.TargetProduct.ProductImages.Select(pi => pi.ImagePath).First(),

                    TargetProductId = user.UserId == ex.SenderId ? ex.TargetProduct.ProductId : ex.CurrentProduct.ProductId,
                    TargetProductName = user.UserId == ex.SenderId ? ex.TargetProduct.ProductName : ex.CurrentProduct.ProductName,
                    TargetProductImage = user.UserId == ex.SenderId ? ex.TargetProduct.ProductImages.Select(pi => pi.ImagePath).First() : ex.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).First(),

                    ReceiverId = ex.Receiver.UserId,
                    ReceiverName = ex.Receiver.FirstName + " " + ex.Receiver.LastName,
                    ReceiverStatus = ex.ReceiverStatus,

                    SenderId = ex.Sender.UserId,
                    SenderName = ex.Sender.FirstName + " " + ex.Sender.LastName,
                    SenderStatus = ex.SenderStatus,

                    UserImage = user.UserId == ex.SenderId ? ex.Receiver.UserImageUrl : ex.Sender.UserImageUrl,

                    Status = ex.Status
                })
                .ToListAsync();

            return result;
        }

    }
}
