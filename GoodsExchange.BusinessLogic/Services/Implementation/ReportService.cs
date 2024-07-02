using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Common.Exceptions;
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

        public async Task<EntityResponse<bool>> ApproveReport(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);
            var userUploadProduct = await  _serviceWrapper.UserServices.GetUserByProductId(report.ProductId);
            //var productbelongReceiverId = await _serviceWrapper.ProductServices.IsProductBelongToSeller(report.ProductId, report.ReceiverId);
            if (report == null)
            {
                throw new NotFoundException("Report does not exist");
            }

            report.IsApprove = true;
            report.IsActive = false;
           
            
            userUploadProduct.IsActive = false;
            

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>(true);
        }

        public async Task<EntityResponse<bool>> DenyReport(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                throw new NotFoundException("Report does not exist");
            }

                
            report.IsActive = false;

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>(true);
        }
        public async Task<EntityResponse<ReportViewModel>> SendReport(CreateReportRequestModel request)
        {
            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var receiver = await _serviceWrapper.UserServices.GetUserByProductId(request.ProductId);

            if (await _serviceWrapper.ProductServices.GetProductAsync(request.ProductId) == null)
            {
                throw new NotFoundException("This product does not exist.");
            }

            if (await _serviceWrapper.ProductServices.IsProductBelongToSeller(request.ProductId, user.UserId))
            {
                throw new BadRequestException("This product belongs to you so you cannot report this product.");
            }

            var report = new Report()
            {
                Reason = request.Reason,
                CreateDate = DateTime.Now,
                SenderId = user.UserId,
                ReceiverId = receiver.UserId,
                ProductId = request.ProductId,
                IsApprove = false,
                IsActive = true
            };

            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();

            var product = await _serviceWrapper.ProductServices.GetProductAsync(request.ProductId);

            var result = new ReportViewModel()
            {

                ReportMade = user.FirstName + " " + user.LastName,
                ReportReceived = await _serviceWrapper.UserServices.GetUserFullNameAsync(report.ReceiverId),
                ReportId = report.ReportId,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Reason = report.Reason,
                IsApprove = report.IsApprove,
                IsActive = report.IsActive
            };
            return new ApiSuccessResult<ReportViewModel>(result);
        }

        public async Task<PageResult<ReportViewModel>> GetReports(PagingRequestModel paging, ReportsRequestModel request)
        {
            var query = _context.Reports.Where(r => r.IsActive == true)
                        .Include(r => r.Sender)
                        .Include(r => r.Receiver)
                        .Include(r => r.Product).AsQueryable();

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
                ReportMade = report.Sender.FirstName + " " + report.Sender.LastName,
                ReportReceived = report.Receiver.FirstName + " " + report.Receiver.LastName,
                ProductId = report.ProductId,
                ProductName = report.Product.ProductName,
                ReportId = report.ReportId,
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

        public async Task<EntityResponse<ReportViewModel>> GetById(Guid id)
        {
            var report = await _context.Reports.FirstOrDefaultAsync(r => r.ReportId == id);
            if (report == null)
            {
                throw new NotFoundException("Report does not exist");
            }

            var result = new ReportViewModel()
            {
                ReportMade = await _serviceWrapper.UserServices.GetUserFullNameAsync(report.SenderId),
                ReportReceived = await _serviceWrapper.UserServices.GetUserFullNameAsync(report.ReceiverId),
                ProductId = (await _serviceWrapper.ProductServices.GetProductAsync(report.ProductId)).ProductId,
                ProductName = (await _serviceWrapper.ProductServices.GetProductAsync(report.ProductId)).ProductName,
                Reason = report.Reason,
                IsApprove = report.IsApprove,
                IsActive = report.IsActive
            };

            return new ApiSuccessResult<ReportViewModel>(result);
        }
    }
}
