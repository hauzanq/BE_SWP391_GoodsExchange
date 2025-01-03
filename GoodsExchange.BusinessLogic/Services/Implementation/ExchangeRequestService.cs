﻿using GoodsExchange.BusinessLogic.Common;
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
                await _serviceWrapper.ProductServices.UpdateProductStatusAsync(request.CurrentProductId, Data.Enums.ProductStatus.Hidden);
                await _serviceWrapper.ProductServices.UpdateProductStatusAsync(request.TargetProductId, Data.Enums.ProductStatus.Hidden);
                request.Status = SystemConstant.ExchangeRequestStatus.Approved;
            }

            if ((request.SenderStatus == 2 && request.ReceiverStatus == 1) || (request.SenderStatus == 1 && request.ReceiverStatus == 2))
            {
                await _serviceWrapper.ProductServices.UpdateProductStatusAsync(request.CurrentProductId, Data.Enums.ProductStatus.Hidden);
                await _serviceWrapper.ProductServices.UpdateProductStatusAsync(request.TargetProductId, Data.Enums.ProductStatus.Hidden);
                request.Status = SystemConstant.ExchangeRequestStatus.Approved;
            }

            if (request.SenderStatus == 2 && request.ReceiverStatus == 2)
            {
                await _serviceWrapper.ProductServices.UpdateProductStatusAsync(request.CurrentProductId, Data.Enums.ProductStatus.Hidden);
                await _serviceWrapper.ProductServices.UpdateProductStatusAsync(request.TargetProductId, Data.Enums.ProductStatus.Hidden);
                request.Status = SystemConstant.ExchangeRequestStatus.Complete;
            }

            await _context.SaveChangesAsync();

            return request.Status;
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

            // Make sure that not exist 2 request duplicated
            if (_context.ExchangeRequests.Any(ex => ex.CurrentProductId == request.CurrentProductId && ex.TargetProductId == request.TargetProductId))
            {
                throw new BadRequestException("The request to exchange between two product have created.");
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
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(10),
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
                CurrentProductDescription = currentProduct.Description,

                TargetProductId = targetProduct.ProductId,
                TargetProductName = targetProduct.ProductName,
                TargetProductImage = targetProduct.ProductImages.Select(pi => pi.ImagePath).First(),
                TargetProductDescription = targetProduct.Description,

                ReceiverId = receiver.UserId,
                ReceiverName = receiver.FirstName + " " + receiver.LastName,
                ReceiverStatus = exchange.ReceiverStatus,

                SenderId = sender.UserId,
                SenderName = sender.FirstName + " " + sender.LastName,
                SenderStatus = exchange.SenderStatus,

                UserImage = sender.UserImageUrl,

                Status = exchange.Status,

                StartTime = exchange.StartTime,
                EndTime = exchange.EndTime
            };

            await _serviceWrapper.EmailServices.SendMailForExchangeRequest(receiver.Email, data);

            return new ResponseModel<ExchangeRequestViewModel>("The exchange request was created successfully.", data);
        }

        public async Task<List<ExchangeRequestViewModel>> GetReceivedExchangeRequestsAsync()
        {
            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var result = await _context.ExchangeRequests
               .Where(ex => ex.ReceiverId == user.UserId && ex.Status != SystemConstant.ExchangeRequestStatus.Cancelled && ex.Status != SystemConstant.ExchangeRequestStatus.Complete)
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
                   CurrentProductDescription = ex.TargetProduct.Description,

                   TargetProductId = ex.CurrentProduct.ProductId,
                   TargetProductName = ex.CurrentProduct.ProductName,
                   TargetProductImage = ex.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).First(),
                   TargetProductDescription = ex.CurrentProduct.Description,

                   ReceiverId = ex.Receiver.UserId,
                   ReceiverName = ex.Receiver.FirstName + " " + ex.Receiver.LastName,
                   ReceiverStatus = ex.ReceiverStatus,

                   SenderId = ex.Sender.UserId,
                   SenderName = ex.Sender.FirstName + " " + ex.Sender.LastName,
                   SenderStatus = ex.SenderStatus,

                   UserImage = ex.Sender.UserImageUrl,

                   Status = ex.Status,

                   StartTime = ex.StartTime,
                   EndTime = ex.EndTime
               })
               .ToListAsync();

            return result;
        }

        public async Task<List<ExchangeRequestViewModel>> GetSentExchangeRequestsAsync()
        {
            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var result = await _context.ExchangeRequests
               .Where(ex => ex.SenderId == user.UserId && ex.Status != SystemConstant.ExchangeRequestStatus.Cancelled && ex.Status != SystemConstant.ExchangeRequestStatus.Complete)
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
                   CurrentProductDescription = ex.CurrentProduct.Description,

                   TargetProductId = ex.TargetProduct.ProductId,
                   TargetProductName = ex.TargetProduct.ProductName,
                   TargetProductImage = ex.TargetProduct.ProductImages.Select(pi => pi.ImagePath).First(),
                   TargetProductDescription = ex.TargetProduct.Description,

                   ReceiverId = ex.Receiver.UserId,
                   ReceiverName = ex.Receiver.FirstName + " " + ex.Receiver.LastName,
                   ReceiverStatus = ex.ReceiverStatus,

                   SenderId = ex.Sender.UserId,
                   SenderName = ex.Sender.FirstName + " " + ex.Sender.LastName,
                   SenderStatus = ex.SenderStatus,

                   UserImage = ex.Receiver.UserImageUrl,

                   Status = ex.Status,

                   StartTime = ex.StartTime,
                   EndTime = ex.EndTime
               })
               .ToListAsync();

            return result;
        }

        public async Task<bool> IsProductInExchangeProcessingAsync(Guid productId)
        {
            return await _context.ExchangeRequests.AnyAsync(ex => ex.TargetProductId == productId);
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

            request.StartTime = DateTime.Now;

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
            await _serviceWrapper.ProductServices.UpdateProductStatusAsync(request.CurrentProductId, Data.Enums.ProductStatus.Approved);
            await _serviceWrapper.ProductServices.UpdateProductStatusAsync(request.TargetProductId, Data.Enums.ProductStatus.Approved);

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

                   CurrentProductId = ex.CurrentProduct.ProductId,
                   CurrentProductName = ex.CurrentProduct.ProductName,
                   CurrentProductImage = ex.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).First(),
                   CurrentProductDescription = ex.CurrentProduct.Description,

                   TargetProductId = ex.TargetProduct.ProductId,
                   TargetProductName = ex.TargetProduct.ProductName,
                   TargetProductImage = ex.TargetProduct.ProductImages.Select(pi => pi.ImagePath).First(),
                   TargetProductDescription = ex.TargetProduct.Description,

                   ReceiverId = ex.Receiver.UserId,
                   ReceiverName = ex.Receiver.FirstName + " " + ex.Receiver.LastName,
                   ReceiverStatus = ex.ReceiverStatus,

                   SenderId = ex.Sender.UserId,
                   SenderName = ex.Sender.FirstName + " " + ex.Sender.LastName,
                   SenderStatus = ex.SenderStatus,

                   UserImage = ex.Receiver.UserImageUrl,

                   Status = ex.Status,

                   StartTime = ex.StartTime,
                   EndTime = ex.EndTime
               })
               .ToListAsync();

            return result;
        }

        public async Task<List<ExchangeRequestViewModel>> GetRejectedExchangeRequestsAsync()
        {
            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var result = await _context.ExchangeRequests
               .Where(ex => ex.SenderId == user.UserId && ex.Status == SystemConstant.ExchangeRequestStatus.Cancelled)
               .Include(ex => ex.CurrentProduct)
               .Include(ex => ex.TargetProduct)
               .Include(ex => ex.Sender)
               .Include(ex => ex.Receiver)
               .Select(ex => new ExchangeRequestViewModel
               {
                   ExchangeRequestId = ex.ExchangeRequestId,

                   // Determine current and target based on user role in the transaction
                   CurrentProductId = ex.SenderId == user.UserId ? ex.CurrentProduct.ProductId : ex.TargetProduct.ProductId,
                   CurrentProductName = ex.SenderId == user.UserId ? ex.CurrentProduct.ProductName : ex.TargetProduct.ProductName,
                   CurrentProductImage = ex.SenderId == user.UserId ? ex.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).FirstOrDefault() : ex.TargetProduct.ProductImages.Select(pi => pi.ImagePath).FirstOrDefault(),
                   CurrentProductDescription = ex.SenderId == user.UserId ? ex.CurrentProduct.Description : ex.TargetProduct.Description,

                   TargetProductId = ex.SenderId == user.UserId ? ex.TargetProduct.ProductId : ex.CurrentProduct.ProductId,
                   TargetProductName = ex.SenderId == user.UserId ? ex.TargetProduct.ProductName : ex.CurrentProduct.ProductName,
                   TargetProductImage = ex.SenderId == user.UserId ? ex.TargetProduct.ProductImages.Select(pi => pi.ImagePath).FirstOrDefault() : ex.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).FirstOrDefault(),
                   TargetProductDescription = ex.SenderId == user.UserId ? ex.TargetProduct.Description : ex.CurrentProduct.Description,

                   // Determine user roles
                   ReceiverId = ex.ReceiverId,
                   ReceiverName = ex.Receiver.FirstName + " " + ex.Receiver.LastName,
                   ReceiverStatus = ex.ReceiverStatus,

                   SenderId = ex.SenderId,
                   SenderName = ex.Sender.FirstName + " " + ex.Sender.LastName,
                   SenderStatus = ex.SenderStatus,

                   // Determine user image
                   UserImage = ex.SenderId == user.UserId ? ex.Receiver.UserImageUrl : ex.Sender.UserImageUrl,

                   Status = ex.Status,

                   StartTime = ex.StartTime,
                   EndTime = ex.EndTime
               })
               .ToListAsync();

            return result;
        }
    }
}
