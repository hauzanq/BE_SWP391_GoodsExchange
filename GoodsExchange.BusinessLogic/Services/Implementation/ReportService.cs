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

        #region For SendReport
        private async Task<bool> UserExists(Guid id)
        {
            //return (await _userService.GetById(id)).Data != null;
            return (await _serviceWrapper.UserServices.GetById(id)).Data != null;
        }
        private async Task<bool> HasPermissionToReport(Guid from, Guid to)
        {
            var receiver = await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.UserId == to);
            if (receiver.UserId == from)
            {
                return false;
            }
            var roles = receiver.UserRoles.Select(u => u.Role.RoleName).ToList();
            if (roles.Any(r => r.Contains(SystemConstant.Roles.Moderator) || r.Contains(SystemConstant.Roles.Administrator)))
            {
                return false;
            }
            return true;
        }
        private async Task<bool> ProductExists(Guid id)
        {
            //return (await _productService.GetById(id)).Data != null;
            return (await _serviceWrapper.ProductServices.GetById(id)).Data != null;
        }
        #endregion

        public async Task<ApiResult<ReportViewModel>> SendReport(CreateReportRequestModel request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            if (!await UserExists(request.ReceiverId))
            {
                return new ApiErrorResult<ReportViewModel>("The report receiver does not exist.");
            }

            if (!await HasPermissionToReport(user.UserId, request.ReceiverId))
            {
                return new ApiErrorResult<ReportViewModel>("You can not report this user.");
            }

            if (!await ProductExists(request.ProductId))
            {
                return new ApiErrorResult<ReportViewModel>("This product does not exist.");
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

            var result = new ReportViewModel()
            {
                ReportMade = user.UserName,
                ReportReceived = _context.Users.FirstOrDefault(u => u.UserId == report.ReceiverId).UserName,
                ProductId = _context.Products.FirstOrDefault(p => p.ProductId == report.ProductId).ProductId,
                ProductName = _context.Products.FirstOrDefault(p => p.ProductId == report.ProductId).ProductName,
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

            var result = new ReportViewModel()
            {
                ReportMade = _context.Users.FirstOrDefault(r => r.UserId == report.SenderId).UserName,
                ReportReceived = _context.Users.FirstOrDefault(u => u.UserId == report.ReceiverId).UserName,
                ProductId = _context.Products.FirstOrDefault(p => p.ProductId == report.ProductId).ProductId,
                ProductName = _context.Products.FirstOrDefault(p => p.ProductId == report.ProductId).ProductName,
                Reason = report.Reason,
                IsApprove = report.IsApprove,
                IsActive = report.IsActive
            };

            return new ApiSuccessResult<ReportViewModel>(result);
        }
    }
}
