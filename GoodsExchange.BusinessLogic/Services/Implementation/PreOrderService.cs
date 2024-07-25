using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Common.Exceptions;
using GoodsExchange.BusinessLogic.Extensions;
using GoodsExchange.BusinessLogic.RequestModels.PreOrder;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.PreOrder;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class PreOrderService : IPreOrderService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceWrapper _serviceWrapper;
        public PreOrderService(GoodsExchangeDbContext context, IHttpContextAccessor httpContextAccessor, IServiceWrapper serviceWrapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceWrapper = serviceWrapper;
        }

        public async Task<ResponseModel<ConfirmPreOrderViewModel>> ConfirmPreOrderAsync(ConfirmPreOrderRequestModel request)
        {
            var preOrder = await _context.PreOrders.Include(po => po.TargetProduct)
                                                .Include(po => po.Buyer)
                                                .Include(po => po.Seller).Where(po => po.PreOrderId == request.PreOrderId).FirstOrDefaultAsync();
            if (preOrder == null)
            {
                throw new NotFoundException("This preorder does not exist.");
            }

            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var seller = await _serviceWrapper.UserServices.GetUserByProductId(preOrder.TargetProductId);

            if (user.UserId == seller.UserId)
            {
                preOrder.SellerConfirmed = true;
                await _context.SaveChangesAsync();
            }
            else
            {
                preOrder.BuyerConfirmed = true;
                await _context.SaveChangesAsync();
            }

            // Create Transaction
            if (preOrder.SellerConfirmed == true && preOrder.BuyerConfirmed == true)
            {
                await _serviceWrapper.TransactionService.CreateTransactionAsync(preOrder.PreOrderId);
            }

            var result = new ConfirmPreOrderViewModel()
            {
                ProductName = preOrder.TargetProduct.ProductName,
                BuyerName = preOrder.Buyer.FirstName + " " + preOrder.Buyer.LastName,
                SellerName = preOrder.Seller.FirstName + " " + preOrder.Seller.LastName,
                BuyerConfirmed = preOrder.BuyerConfirmed,
                SellerConfirmed = preOrder.SellerConfirmed
            };

            return new ResponseModel<ConfirmPreOrderViewModel>("Confirm preorder successfully.", result);
        }

        public async Task<ResponseModel<PreOrderViewModel>> CreatePreOrderAsync(CreatePreOrderRequestModel request)
        {
            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                throw new NotFoundException("The product does not exist.");
            }

            var buyer = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            var seller = await _serviceWrapper.UserServices.GetUserByProductId(product.ProductId);

            var preOrder = new PreOrder()
            {
                TargetProductId = product.ProductId,
                BuyerId = buyer.UserId,
                SellerId = seller.UserId,
                Status = true
            };

            await _context.PreOrders.AddAsync(preOrder);
            await _context.SaveChangesAsync();

            var data = new PreOrderViewModel()
            {
                UserName = buyer.FirstName + " " + buyer.LastName,
                ProductName = product.ProductName,
                ProductImage = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                ProductPrice = product.Price
            };

            return new ResponseModel<PreOrderViewModel>("The preorder was created successfully.", data);
        }

        public async Task<ResponseModel<List<PreOrderViewModel>>> GetPreOrdersForProductAsync(Guid productid)
        {
            var product = await _context.Products.FindAsync(productid);
            if (product == null)
            {
                throw new NotFoundException("The product does not exist.");
            }

            var data = await _context.PreOrders.Include(po => po.TargetProduct)
                                                .Include(po => po.Buyer)
                                                .Include(po => po.Seller)
                                                .Where(po => po.TargetProductId == productid).ToListAsync();

            var result = data.Select(po => new PreOrderViewModel()
            {
                UserName = po.Buyer.FirstName + " " + po.Buyer.LastName,
                ProductName = po.TargetProduct.ProductName,
                ProductImage = po.TargetProduct.ProductImages.Select(pi => pi.ImagePath).ToList(),
                ProductPrice = po.TargetProduct.Price
            }).ToList();

            return new ResponseModel<List<PreOrderViewModel>>(result);
        }

        public async Task<List<PreOrderViewModel>> GetReceivedExchangeRequestsByProductIdAsync(Guid productId)
        {
            var result = await _context.PreOrders
               .Where(po => po.TargetProductId == productId)
               .Include(po => po.CurrentProduct)
               .Include(po => po.TargetProduct)
               .Include(po => po.Buyer)
               .Include(po => po.Seller)
               .Select(po => new PreOrderViewModel
               {
                   UserName = po.Buyer.FirstName + " " + po.Buyer.LastName,
                   ProductName = po.CurrentProduct.ProductName,
                   ProductImage = po.CurrentProduct.ProductImages.Select(pi => pi.ImagePath).ToList(),
                   ProductPrice = po.CurrentProduct.Price
               })
               .ToListAsync();

            return result;
        }

        public async Task<List<PreOrderViewModel>> GetSentExchangeRequestsByProductIdAsync(Guid productId)
        {
            var result = await _context.PreOrders
               .Where(po => po.CurrentProductId == productId)
               .Include(po => po.CurrentProduct)
               .Include(po => po.TargetProduct)
               .Include(po => po.Buyer)
               .Include(po => po.Seller)
               .Select(po => new PreOrderViewModel
               {
                   UserName = po.Buyer.FirstName + " " + po.Buyer.LastName,
                   ProductName = po.TargetProduct.ProductName,
                   ProductImage = po.TargetProduct.ProductImages.Select(pi => pi.ImagePath).ToList(),
                   ProductPrice = po.TargetProduct.Price
               })
               .ToListAsync();

            return result;
        }

        public async Task<bool> IsProductInPreOrderAsync(Guid productId)
        {
            return await _context.PreOrders.AnyAsync(po => po.TargetProductId == productId);
        }
    }
}
