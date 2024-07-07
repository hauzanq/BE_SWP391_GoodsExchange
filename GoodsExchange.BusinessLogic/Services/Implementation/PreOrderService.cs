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
            var preOrder = await _context.PreOrders.Include(po => po.Product)
                                                .Include(po => po.Buyer)
                                                .Include(po => po.Seller).Where(po => po.PreOrderId == request.PreOrderId).FirstOrDefaultAsync();
            if (preOrder == null)
            {
                throw new NotFoundException("This preorder does not exist.");
            }

            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var seller = await _serviceWrapper.UserServices.GetUserByProductId(preOrder.ProductId);

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
                ProductName = preOrder.Product.ProductName,
                BuyerName = preOrder.Buyer.FirstName + " " + preOrder.Buyer.LastName,
                SellerName = preOrder.Seller.FirstName + " " + preOrder.Seller.LastName,
                BuyerConfirmed = preOrder.BuyerConfirmed,
                SellerConfirmed = preOrder.SellerConfirmed
            };

            return new ResponseModel<ConfirmPreOrderViewModel>("Confirm preorder successfully.",result);
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
                ProductId = product.ProductId,
                BuyerId = buyer.UserId,
                SellerId = seller.UserId,
                IsActive = true,
                DateCreated = DateTime.UtcNow,
            };

            await _context.PreOrders.AddAsync(preOrder);
            await _context.SaveChangesAsync();

            var data = new PreOrderViewModel()
            {
                BuyerName = buyer.FirstName + " " + buyer.LastName,
                ProductName = product.ProductName
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

            var data = await _context.PreOrders.Include(po => po.Product)
                                                .Include(po => po.Buyer)
                                                .Include(po => po.Seller)
                                                .Where(po => po.ProductId == productid).ToListAsync();

            var result = data.Select(po => new PreOrderViewModel()
            {
                BuyerName = po.Buyer.FirstName + " " + po.Buyer.LastName,
                ProductName = po.Product.ProductName
            }).ToList();

            return new ResponseModel<List<PreOrderViewModel>>(result);
        }
    }
}
