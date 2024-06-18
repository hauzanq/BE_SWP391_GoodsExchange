using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.Extensions;
using GoodsExchange.BusinessLogic.RequestModels.Report;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Report;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class ReportService : IReportService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceWrapper _serviceWrapper;

        public ReportService(GoodsExchangeDbContext context, IHttpContextAccessor httpContextAccessor, IServiceWrapper serviceWrapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceWrapper = serviceWrapper;
        }

        public async Task<ApiResult<bool>> ApproveReport(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return new ApiErrorResult<bool>("Report does not exist");
            }

            report.IsApprove = true;
            report.IsActive = false;

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>(true);
        }
        private async Task<bool> IsProductBelongToSeller(Guid productId, Guid sellerId)
        {
            var seller = await _context.Users.Include(u => u.Products).FirstOrDefaultAsync(u => u.UserId == sellerId);
            return seller != null && seller.Products.Any(p => p.ProductId == productId);
        }
        public async Task<ApiResult<ReportViewModel>> SendReport(CreateReportRequestModel request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            if (!(await _serviceWrapper.RoleServices.HasPermissionToReportAndRating(user.UserId, request.ReceiverId)))
            {
                return new ApiErrorResult<ReportViewModel>("You can not report this user.");
            }

            if (await _serviceWrapper.ProductServices.GetProductAsync(request.ProductId) == null)
            {
                return new ApiErrorResult<ReportViewModel>("This product does not exist.");
            }

            if (!(await IsProductBelongToSeller(request.ProductId, request.ReceiverId)))
            {
                return new ApiErrorResult<ReportViewModel>("This product is not belong to this seller.");
            }

            var report = new Report()
            {
                Reason = request.Reason,
                CreateDate = DateTime.Now,
                SenderId = user.UserId,
                ReceiverId = request.ReceiverId,
                ProductId = request.ProductId,
                IsApprove = false,
                IsActive = true
            };

            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();

            var product = await _serviceWrapper.ProductServices.GetProductAsync(request.ProductId);

            var result = new ReportViewModel()
            {
                ReportMade = user.UserName,
                ReportReceived = _serviceWrapper.UserServices.GetUserAsync(report.ReceiverId).Result.UserName,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Reason = report.Reason,
                IsApprove = report.IsApprove,
                IsActive = report.IsActive
            };
            return new ApiSuccessResult<ReportViewModel>(result);
        }

        public async Task<PageResult<ReportViewModel>> GetAll(PagingRequestModel paging, ReportsRequestModel request)
        {
            var query = _context.Reports.Where(r => r.IsActive == true).AsQueryable();

            #region Filtering report

            if (!string.IsNullOrEmpty(request.Sender))
            {
                query = query.Where(r => r.Sender.UserName.Contains(request.Sender));
            }

            if (!string.IsNullOrEmpty(request.Receiver))
            {
                query = query.Where(r => r.Receiver.UserName.Contains(request.Receiver));
            }

            if (request.FromDate != null)
            {
                query = query.Where(r => r.CreateDate >= request.FromDate);
            }

            if (request.ToDate != null)
            {
                query = query.Where(r => r.CreateDate <= request.ToDate);
            }

            #endregion

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / paging.PageSize);

            var data = query.Select(report => new ReportViewModel()
            {
                ReportMade = _context.Users.FirstOrDefault(r => r.UserId == report.SenderId).UserName,
                ReportReceived = _context.Users.FirstOrDefault(u => u.UserId == report.ReceiverId).UserName,
                ProductId = _context.Products.FirstOrDefault(p => p.ProductId == report.ProductId).ProductId,
                ProductName = _context.Products.FirstOrDefault(p => p.ProductId == report.ProductId).ProductName,
                Reason = report.Reason,
                IsApprove = report.IsApprove,
                IsActive = report.IsActive
            }).ToList();

            var result = new PageResult<ReportViewModel>()
            {
                Items = data,
                TotalPage = totalPages,
                CurrentPage = paging.PageIndex
            };

            return result;
        }

        public async Task<ApiResult<ReportViewModel>> GetById(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return new ApiErrorResult<ReportViewModel>("Report does not exist");
            }

            var sender = await _serviceWrapper.UserServices.GetUserAsync(report.Sender.UserId);
            var receiver = await _serviceWrapper.UserServices.GetUserAsync(report.Receiver.UserId);
            var product = await _serviceWrapper.ProductServices.GetProductAsync(report.ProductId);
            
            var result = new ReportViewModel()
            {
                ReportMade = sender.FirstName + " " + sender.LastName,
                ReportReceived = sender.FirstName + " " + sender.LastName,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Reason = report.Reason,
                IsApprove = report.IsApprove,
                IsActive = report.IsActive
            };

            return new ApiSuccessResult<ReportViewModel>(result);
        }
    }
}
