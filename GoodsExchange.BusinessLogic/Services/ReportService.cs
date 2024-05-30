using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Report;
using GoodsExchange.BusinessLogic.ViewModels;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Services
{

    public interface IReportService
    {
        Task<ApiResult<ReportViewModel>> SendReport(CreateReportRequestModel request);
        Task<ApiResult<bool>> ApproveReport(Guid id);
        Task<PageResult<ReportViewModel>> GetAll(PagingRequestModel paging);
        Task<ApiResult<ReportViewModel>> GetById(Guid id);
    }

    public class ReportService : IReportService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        public ReportService(GoodsExchangeDbContext context, IUserService userService, IProductService productService)
        {
            _context = context;
            _userService = userService;
            _productService = productService;
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

            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<ReportViewModel>> SendReport(CreateReportRequestModel request)
        {
            var report = new Report()
            {
                ReportingUserId = request.ReportingUserId,
                TargetUserId = request.TargetUserId,
                Reason = request.Reason,
                ProductId = request.ProductId,
                IsActive = true,
                IsApprove = false
            };

            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();

            var result = new ReportViewModel()
            {
                ReportMade = _userService.GetById(report.ReportingUserId).Result.Data.UserName.ToString(),
                ReportReceived = _userService.GetById(report.TargetUserId).Result.Data.UserName.ToString(),
                ProductId = _productService.GetById(report.ProductId).Result.Data.ProductId,
                ProductName = _productService.GetById(report.ProductId).Result.Data.ProductName.ToString(),
                Reason = report.Reason,
                IsApprove = false,
                IsActive = true
            };
            return new ApiSuccessResult<ReportViewModel>(result);
        }

        public async Task<PageResult<ReportViewModel>> GetAll(PagingRequestModel paging)
        {
            var reports = await _context.Reports.Where(r=>r.IsActive == true).ToListAsync();

            var totalItems = reports.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / paging.PageSize);

            var data = reports.Select(report => new ReportViewModel()
            {
                ReportMade = _userService.GetById(report.ReportingUserId).Result.Data.UserName.ToString(),
                ReportReceived = _userService.GetById(report.ReportingUserId).Result.Data.UserName.ToString(),
                ProductId = _productService.GetById(report.ProductId).Result.Data.ProductId,
                ProductName = _productService.GetById(report.ProductId).Result.Data.ProductName.ToString(),
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
                ReportMade = _userService.GetById(report.ReportingUserId).Result.Data.UserName.ToString(),
                ReportReceived = _userService.GetById(report.ReportingUserId).Result.Data.UserName.ToString(),
                ProductId = _productService.GetById(report.ProductId).Result.Data.ProductId,
                ProductName = _productService.GetById(report.ProductId).Result.Data.ProductName.ToString(),
                Reason = report.Reason,
                IsApprove = report.IsApprove,
                IsActive = report.IsActive
            };

            return new ApiSuccessResult<ReportViewModel> (result);
        }
    }
}
